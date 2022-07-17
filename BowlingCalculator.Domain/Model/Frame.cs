using BowlingCalculator.Domain.FrameStates;

namespace BowlingCalculator.Domain
{
    public class Frame
    {
        public IFrameState FrameState { get; private set; }
        public byte FrameNumber { get; }

        public Frame(byte frameNumber, IFrameState frameState)
        {
            FrameNumber = frameNumber;
            FrameState = frameState;
        }

        public void ApplyRoll(byte pinsDowned,byte maxPins)
        {
            IFrameState frameState = FrameState.ApplyRoll(pinsDowned,maxPins);
            FrameState = frameState;            
        }

        public byte GetFrameCompletedScore()
        {
            byte completedScore = 0;
            if (FrameState.IsScoringCompleted())
                completedScore = FrameState.FrameScore.Value;
            return completedScore;
        }
    }
}
