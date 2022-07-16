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

        public static Frame CreateDefaultFrame(byte frameNumber)
        {
            return new Frame(frameNumber, OpenFrame.CreateDefaultOpenFrame());
        }
    }
}
