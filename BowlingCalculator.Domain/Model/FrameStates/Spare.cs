namespace BowlingCalculator.Domain.FrameStates
{
    public class Spare : IFrameState
    {
        public FrameStateType FrameStateType { get { return FrameStateType.Spare; } }
        public byte? FrameScore { get; private set; }
        public byte? FirstRoll { get; }
        public byte? SecondRoll { get; }
        public byte? FirstBonusRoll { get; private set; }

        public Spare(byte? firstRoll, byte? secondRoll, byte frameScore,
                     byte? firstBonusRoll)
        {
            FrameScore = frameScore;
            FirstRoll = firstRoll;
            SecondRoll = secondRoll;
            FirstBonusRoll = firstBonusRoll;
        }

        public IFrameState ApplyRoll(byte pinsDowned,byte maxPins)
        {
            return ApplyBonusRoll(pinsDowned);
        }

        public bool ShouldTransitionToNextFrame()
        {
            return true;
        }

        public bool IsScoringCompleted()
        {
            return FirstBonusRoll.HasValue;
        }

        private IFrameState ApplyBonusRoll(byte pinsDowned)
        {
            FirstBonusRoll = pinsDowned;
            FrameScore+=FirstBonusRoll.Value;
            return this;
        }       
    }
}
