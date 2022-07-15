using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingCalculator.Domain
{
    public class Game
    {
        public short RunningTotal { get; private set; }

        public LinkedList<Frame> Frames { get; }

        private Frame _currentFrame;

        public bool GameFinished { get; private set; }

        private Game(short score,LinkedList<Frame> frames)
        {
            RunningTotal = score;
            Frames = frames;
        }

        public void Throw(byte pinsDowned)
        {
            if (GameFinished)
                throw new Exception();

            _currentFrame.ApplyPinsDowned(pinsDowned);

            ApplyScoringToPreviousFrames(pinsDowned);
            
            if(_currentFrame.FrameCompleted)
                SetNextFrame();
        }

        private void SetNextFrame()
        {
            Frame nextFrame = Frames.Find(_currentFrame).Next?.Value;
            if (nextFrame != null)
                _currentFrame = nextFrame;
            else if (_currentFrame.FrameScoreCalculated)
                GameFinished = true;
            //else this is 10th frame with strike
        }

        private void ApplyScoringToPreviousFrames(byte pinsDowned)
        {
            Frame frameToCheckScoring = _currentFrame;
            bool checkPrevious = true;
            while (checkPrevious)
            {
                Frame frame = Frames.Find(frameToCheckScoring).Previous?.Value;
                if (frame != null && !frame.FrameScoreCalculated)
                {
                    frame.ApplyPinsDowned(pinsDowned);
                    frameToCheckScoring = frame;
                }
                else
                {
                    checkPrevious = false;
                }
            }
        }
    }
}
