namespace BowlingCalculator.Domain.FrameStates
{
    public interface IFrameState
    {
        FrameStateType FrameStateType { get; }
        byte? FrameScore { get; }
        IFrameState ApplyRoll(byte pinsDowned,byte maxPins);
        bool ShouldTransitionToNextFrame();
    }

    public enum FrameStateType
    {
        OpenFrame,
        Spare,
        Strike
    }
}
