namespace BowlingCalculator.Domain.FrameStates
{
    public class Strike : IFrameState
    {
        public FrameStateType FrameStateType { get { return FrameStateType.Strike; } }
        public byte? FrameScore { get; private set; }
        public byte? FirstRoll { get; }
        public byte? FirstBonusRoll { get; private set; }
        public byte? SecondBonusRoll { get; private set; }

        public Strike(byte frameScore, byte? firstRoll, 
                      byte? firstBonusRoll, byte? secondBonusRoll)
        {
            FrameScore = frameScore;
            FirstRoll = firstRoll;
            FirstBonusRoll = firstBonusRoll;
            SecondBonusRoll = secondBonusRoll;
        }

        public IFrameState ApplyRoll(byte pinsDowned,byte maxPins)
        {
            if (!FirstBonusRoll.HasValue)
                return ApplyFirstBonusRoll(pinsDowned);
            else
                return ApplySecondBonusRoll(pinsDowned);
        }

        public bool ShouldTransitionToNextFrame()
        {
            return true;
        }

        public bool IsScoringCompleted()
        {
            return SecondBonusRoll.HasValue;
        }

        private IFrameState ApplyFirstBonusRoll(byte pinsDown)
        {
            FirstBonusRoll = pinsDown;

            FrameScore += FirstBonusRoll.Value;

            return this;
        }

        private IFrameState ApplySecondBonusRoll(byte pinsDown)
        {
            SecondBonusRoll = pinsDown;
            FrameScore += SecondBonusRoll.Value;
            return this;
        }
       
    }
}
