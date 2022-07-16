namespace BowlingCalculator.Domain.FrameStates
{
    public class OpenFrame : IFrameState
    {
        public FrameStateType FrameStateType { get { return FrameStateType.OpenFrame; } }
        public byte? FrameScore { get; private set; }
        public byte? FirstRoll { get; private set; }
        public byte? SecondRoll { get; private set; }        

        public OpenFrame(byte? frameScore, byte? firstRoll, byte? secondRoll)
        {
            FrameScore = frameScore;
            FirstRoll = firstRoll;
            SecondRoll = secondRoll;
        }

        public IFrameState ApplyRoll(byte pinsDowned,byte maxPins)
        {
            if (!FirstRoll.HasValue)
                return ApplyFirstRoll(pinsDowned,maxPins);
            else
                return ApplySecondRoll(pinsDowned, maxPins);
        }

        public bool ShouldTransitionToNextFrame()
        {
            return FirstRoll.HasValue && SecondRoll.HasValue;
        }

        private IFrameState ApplyFirstRoll(byte pinsDown,byte maxPins)
        {
            FirstRoll = pinsDown;

            if (FirstRoll == maxPins)
                return TransitionToStrikeState();

            return this;
        }       

        private IFrameState ApplySecondRoll(byte pinsDown,byte maxPins)
        {
            SecondRoll = pinsDown;

            if (FirstRoll.Value + SecondRoll.Value == maxPins)
                return TransitionToSpareState();

            CalculateScore();//points are calulated when both throws are done

            return this;
        }

        private void CalculateScore()
        {
            FrameScore = (byte)(FirstRoll.Value + SecondRoll.Value);
        }

        private IFrameState TransitionToSpareState()
        {
            return Spare.CreateDefaultSpare(FirstRoll.Value, SecondRoll.Value);
        }

        private IFrameState TransitionToStrikeState()
        {
            return Strike.CreateDefaultStrike(FirstRoll.Value);
        }

        public static OpenFrame CreateDefaultOpenFrame()
        {
            return new OpenFrame(frameScore: null,firstRoll: null, secondRoll: null);
        }

       
    }
}
