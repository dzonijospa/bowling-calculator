using BowlingCalculator.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingCalculator.Domain
{
    public enum GameStatus
    {
        NotStarted,
        InProgress,
        Finished
    }

    public class Game
    {
        public LinkedList<Frame> Frames { get; }        
        public byte MaxPins { get; }
        public byte CurrentFrameNumber { get; private set; }
        public GameStatus GameStatus { get; private set; }
        public short RunningTotal { get; private set; }

        public Guid Id { get; }

        private readonly object _lock = new object();

        public Game(Guid id,LinkedList<Frame> frames,byte currentFrameNumber,byte maxPins, GameStatus status, short runningTotal)
        {
            Id = id;
            Frames = frames;
            CurrentFrameNumber = currentFrameNumber;
            MaxPins = maxPins;
            GameStatus = status;
            RunningTotal = runningTotal;
        }

        /// <summary>
        ///  Rolls pins  
        /// </summary>
        /// <param name="pinsDowned">Number of pins downed in roll</param>
        /// <exception cref="GameFinishedException">If roll happens after game is finished</exception>
        /// <exception cref="InvalidNumberOfPinsException"></exception>
        /// <remarks>Thread safe</remarks>
        public void Roll(byte pinsDowned)
        {
            ValidatePins(pinsDowned);
            lock (_lock)//only method that alters state
            {
                ValidateGameNotFinished();

                Frame currentFrame = Frames.First(x => x.FrameNumber == CurrentFrameNumber);

                bool isLastFrame = Frames.Last.Value.FrameNumber == CurrentFrameNumber;

                ApplyRollToFramesAndSetRunningScore(currentFrame, pinsDowned,MaxPins);

                SetCurrentFrameNumber(currentFrame, isLastFrame);

                SetGameStatus();
            }
        }     

        public bool IsGameCompleted()
        {
            return GameStatus == GameStatus.Finished;
        }

        private void ApplyRollToFramesAndSetRunningScore(Frame currentFrame,byte pinsDowned,byte maxPins)
        {
            ApplyRollAndUpdateRunningScoreForFrame(currentFrame, pinsDowned, maxPins);

            Frame frameToCheckPreviusNode = currentFrame;
            bool checkPreviousNode = true;
            //we will check previous nodes and apply roll to ones that didn't complete scoring 
            while (checkPreviousNode)
            {
                Frame previousFrame = Frames.Find(frameToCheckPreviusNode).Previous?.Value;
                if (previousFrame != null && 
                    !previousFrame.FrameState.IsScoringCompleted())
                {
                    ApplyRollAndUpdateRunningScoreForFrame(previousFrame, pinsDowned, maxPins);
                    frameToCheckPreviusNode = previousFrame;
                }
                else//no previous frame can have scoring not completed
                {
                    checkPreviousNode = false;
                }
            }
        }    

        private void ApplyRollAndUpdateRunningScoreForFrame(Frame frame,byte pinsDowned,byte MaxPins)
        {
            frame.ApplyRoll(pinsDowned, MaxPins);
            RunningTotal += frame.GetFrameCompletedScore();
        }

        private void SetCurrentFrameNumber(Frame currentFrame,bool isLastFrame)
        {
            byte currentFrameNumber = CurrentFrameNumber;
            if (!isLastFrame && 
                currentFrame.FrameState.ShouldTransitionToNextFrame())
            {
                Frame nextFrame = Frames.Find(currentFrame).Next.Value;
                currentFrameNumber = nextFrame.FrameNumber;
            }
            CurrentFrameNumber = currentFrameNumber;
        }

        private void SetGameStatus()
        {
            if (Frames.Last.Value.FrameState.IsScoringCompleted())
                GameStatus = GameStatus.Finished;
            else
                GameStatus = GameStatus.InProgress;
        }

        private void ValidatePins(byte pinsDowned)
        {
            if (pinsDowned > MaxPins)
                throw new InvalidNumberOfPinsException(pinsDowned.ToString());
        }

        private void ValidateGameNotFinished()
        {
            if (IsGameCompleted())
                throw new GameFinishedException();
        }

    }
}
