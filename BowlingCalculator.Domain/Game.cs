using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingCalculator.Domain
{
    public class Game
    {
        public LinkedList<Frame> Frames { get; }        
        public byte MaxPins { get; }
        public byte CurrentFrameNumber { get; private set; }
        public short RunningTotal { get; private set; }
        public long GameId { get; }

        private readonly object _lock = new object();

        public Game(long gameId,LinkedList<Frame> frames,byte currentFrameNumber,byte maxPins)
        {
            GameId = gameId;
            Frames = frames;
            CurrentFrameNumber = currentFrameNumber;
            MaxPins = maxPins;
            CalculateRunningTotal();
        }

        public void Roll(byte pinsDowned)
        {
            ValidateRoll(pinsDowned);
            lock (_lock)//only method that alters state
            {
                Frame currentFrame = Frames.First(x => x.FrameNumber == CurrentFrameNumber);

                bool isLastFrame = Frames.Last.Value.FrameNumber == CurrentFrameNumber;

                ApplyRollToCurrentFrame(currentFrame, pinsDowned);

                ApplyRollToPreviousFrames(currentFrame, pinsDowned);

                CalculateRunningTotal();

                SetCurrentFrame(currentFrame, isLastFrame);
            }
        }      

        public bool IsGameFinished()
        {
            return FrameScoreCompleted(Frames.Last.Value);
        }

        private void CalculateRunningTotal()
        {
            short runningTotal = 0;
            foreach (Frame frame in Frames)
            {
                if (!FrameScoreCompleted(frame))
                    break; //no subsequent frame can have score

                runningTotal += frame.FrameState.FrameScore.Value;
            }
            RunningTotal = runningTotal;
        }

        private void ApplyRollToCurrentFrame(Frame currentFrame,byte pinsDowned)
        {
            currentFrame.ApplyRoll(pinsDowned,MaxPins);
        }

        private void ApplyRollToPreviousFrames(Frame currentFrame,byte pinsDowned)
        {
            Frame frameToCheckScoringForPreviusNode = currentFrame;
            bool checkPreviousNode = true;
            while (checkPreviousNode)
            {
                Frame previousFrame = Frames.Find(frameToCheckScoringForPreviusNode).Previous?.Value;
                if (previousFrame != null && !FrameScoreCompleted(previousFrame))
                {
                    previousFrame.ApplyRoll(pinsDowned,MaxPins);
                    frameToCheckScoringForPreviusNode = previousFrame;
                }
                else
                {
                    checkPreviousNode = false;
                }
            }
        }

        private void ValidateRoll(byte pinsDowned)
        {
            if (IsGameFinished())
                throw new Exception("Game is finished!");

            if(pinsDowned > MaxPins)
                throw new Exception("Invalid number of pins!");
        }

        private void SetCurrentFrame(Frame currentFrame,bool isLastFrame)
        {
            if (!isLastFrame && currentFrame.FrameState.ShouldTransitionToNextFrame())
            {                
                ChangeCurrentFrameToNext(currentFrame);
            }
        }

        private void ChangeCurrentFrameToNext(Frame currentFrame)
        {
            Frame nextFrame = Frames.Find(currentFrame).Next.Value;
            CurrentFrameNumber = nextFrame.FrameNumber;
        }

        private bool FrameScoreCompleted(Frame frame)
        {
            return frame.FrameState.FrameScore.HasValue;
        }
    }
}
