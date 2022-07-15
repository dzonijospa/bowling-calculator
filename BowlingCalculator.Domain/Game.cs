using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingCalculator.Domain
{
    public class Game
    {
        public LinkedList<Frame> Frames { get; }

        public Game(LinkedList<Frame> frames)
        {
            Frames = frames;
        }

        public bool IsGameFinished()
        {
            return Frames.Last.Value.FrameState.IsScoreCalculated;
        }

        public short CalculateRunningTotal()
        {
            short runningTotal = 0;
            foreach (Frame frame in Frames)
            {
                runningTotal += frame.FrameState.FrameScore;
            }
            return runningTotal;
        }

        public void Roll(byte pinsDowned)
        {
            if (IsGameFinished())
                throw new Exception();

            Frame currentFrame = Frames.First(x => x.IsCurrentFrame);

            ApplyScoringToCurrentFrame(currentFrame, pinsDowned);

            ApplyScoringToPreviousFrames(pinsDowned, currentFrame);
            
            SetCurrentFrame(currentFrame);
        }

        private void ApplyScoringToCurrentFrame(Frame currentFrame,byte pinsDowned)
        {
            currentFrame.ApplyPinsDowned(pinsDowned);
        }

        private void ApplyScoringToPreviousFrames(byte pinsDowned,Frame currentFrame)
        {
            Frame frameToCheckScoringForPreviusNode = currentFrame;
            bool checkPreviousNode = true;
            while (checkPreviousNode)
            {
                Frame previousFrame = Frames.Find(frameToCheckScoringForPreviusNode).Previous?.Value;
                if (previousFrame != null && !previousFrame.FrameState.IsScoreCalculated)
                {
                    previousFrame.ApplyPinsDowned(pinsDowned);
                    frameToCheckScoringForPreviusNode = previousFrame;
                }
                else
                {
                    checkPreviousNode = false;
                }
            }
        }

        private void SetCurrentFrame(Frame currentFrame)
        {
            if (!currentFrame.FrameState.ThrowingDoneForFrame)
                return;

            Frame nextFrame = Frames.Find(currentFrame).Next?.Value;
            if (nextFrame != null)
                ChangeCurrentFrameValue(currentFrame,nextFrame);
        }

        private void ChangeCurrentFrameValue(Frame currentFrame, Frame nextFrame)
        {
            currentFrame.SetCurrentFrame(false);
            nextFrame.SetCurrentFrame(true);
        }

       

        //public static Game CreateTenPinGame()
        //{
        //    LinkedList<Frame> frames = CreateFrames();

        //    return new Game(0, frames);
        //}

        //private static LinkedList<Frame> CreateFrames()
        //{
        //    var frames = new LinkedList<Frame>();
        //    var lastNodeAdded = frames.AddFirst(Frame.CreateFrame(1));

        //    for (byte i = 2; i <= 9; i++)
        //    {
        //        var frame = Frame.CreateFrame(i);
        //        lastNodeAdded = frames.AddAfter(lastNodeAdded, frame);
        //    }

        //    frames.AddLast(Frame.CreateFrame(10));
        //    return frames;
        //}
    }
}
