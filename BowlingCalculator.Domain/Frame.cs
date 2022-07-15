using BowlingCalculator.Domain.FrameStates;

namespace BowlingCalculator.Domain
{
    public class Frame
    {
        public bool IsCurrentFrame { get; private set; }
        public IFrameState FrameState { get; private set; }

        public byte FrameNumber { get; }

        public Frame(byte frameNumber, IFrameState frameState,bool isCurrentFrame)
        {
            FrameNumber = frameNumber;
            FrameState = frameState;
            IsCurrentFrame = isCurrentFrame;
        }

        public void SetCurrentFrame(bool currentFrameFlag)
        {
            IsCurrentFrame = currentFrameFlag;
        }

        public void ApplyPinsDowned(byte pinsDowned)
        {
            IFrameState frameState = FrameState.ApplyPinsDowned(pinsDowned);
            FrameState = frameState;            
        }
    }
}
