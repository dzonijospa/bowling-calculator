namespace BowlingCalculator.Domain.FrameStates
{
    public class Spare : IFrameState
    {
        public FrameStateType FrameStateType { get { return FrameStateType.Spare; } }
        public byte? FrameScore { get; private set; }
        public byte? FirstRoll { get; }
        public byte? SecondRoll { get; }
        public byte? FirstBonusRoll { get; private set; }

        public Spare(byte? frameScore, byte? firstRoll, byte? secondRoll,
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

        private IFrameState ApplyBonusRoll(byte pinsDowned)
        {
            FirstBonusRoll = pinsDowned;
            CalculateScore();//points are calulated when bonus roll is done
            return this;
        }    

        private void CalculateScore()
        {
            FrameScore = (byte)(FirstRoll.Value + SecondRoll.Value + FirstBonusRoll.Value);
        }

        public static Spare CreateDefaultSpare( byte firstRoll, byte secondRoll)
        {
            return new Spare(firstRoll: firstRoll, secondRoll: secondRoll, 
                            frameScore: null, firstBonusRoll: null);
        }
    }
}
