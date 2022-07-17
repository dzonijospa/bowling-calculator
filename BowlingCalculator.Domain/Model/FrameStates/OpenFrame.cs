using BowlingCalculator.Domain.Exceptions;

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

        public bool IsScoringCompleted()
        {
            return FirstRoll.HasValue && SecondRoll.HasValue;
        }

        public bool ShouldTransitionToNextFrame()
        {
            return FirstRoll.HasValue && SecondRoll.HasValue;
        }

        private IFrameState ApplyFirstRoll(byte pinsDown,byte maxPins)
        {
            FirstRoll = pinsDown;
            FrameScore = FirstRoll.Value;

            if (FrameScore == maxPins)
                return TransitionToStrikeState();

            return this;
        }       

        private IFrameState ApplySecondRoll(byte pinsDown,byte maxPins)
        {
            ValidateNumberOfPins(pinsDown, maxPins);

            SecondRoll = pinsDown;
            FrameScore += SecondRoll.Value;

            if (FrameScore == maxPins)
                return TransitionToSpareState();

            return this;
        }

       

        private IFrameState TransitionToSpareState()
        {
            return new Spare(firstRoll: FirstRoll.Value, secondRoll: SecondRoll.Value,
                            frameScore: FrameScore.Value, firstBonusRoll: null);
        }

        private IFrameState TransitionToStrikeState()
        {
            return new Strike(firstRoll: FirstRoll.Value,frameScore: FrameScore.Value,
                firstBonusRoll: null, secondBonusRoll: null);
        }
        private void ValidateNumberOfPins(byte pinsDown, byte maxPins)
        {
            if (FirstRoll.Value + pinsDown > maxPins)
                throw new InvalidNumberOfPinsException(pinsDown.ToString());
        }

    }
}
