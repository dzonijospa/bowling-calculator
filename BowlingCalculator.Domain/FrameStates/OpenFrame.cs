using System;

namespace BowlingCalculator.Domain.FrameStates
{
    public class OpenFrame : IFrameState
    {

        public byte MaxPins { get; }
        public bool ThrowingDoneForFrame { get; private set; }

        public bool IsScoreCalculated { get; private set; } = false;

        public byte FrameScore { get; private set; }

        public byte? FirstThrow { get; private set; }

        public byte? SecondThrow { get; private set; }

        public OpenFrame(byte maxPins,bool throwingDone,bool isScoringCompleted,byte frameScore,byte? firstThrow,byte? secondThrow)
        {
            MaxPins = maxPins;
            ThrowingDoneForFrame = throwingDone;
            IsScoreCalculated = isScoringCompleted;
            FrameScore = frameScore;
            FirstThrow = firstThrow;
            SecondThrow = secondThrow;
        }

        public IFrameState ApplyPinsDowned(byte pinsDowned)
        {
            if (!FirstThrow.HasValue)
                return ApplyFirstThrow(pinsDowned);
            else
                return ApplySecondThrow(pinsDowned);
        }

        private IFrameState ApplyFirstThrow(byte pinsDown)
        {
            FirstThrow = pinsDown;           

            if (FirstThrow == MaxPins)
                return Strike.CreateDefaultStrike(MaxPins);

            FrameScore += FirstThrow.Value;

            return this;
        }

        private IFrameState ApplySecondThrow(byte pinsDown)
        {
            SecondThrow = pinsDown;            

            if (FirstThrow.Value + SecondThrow.Value == MaxPins)
                return Spare.CreateDefaultSpare(MaxPins, FirstThrow,SecondThrow);

            FrameScore += SecondThrow.Value;
            ThrowingDoneForFrame = true;
            IsScoreCalculated = true;

            return this;
        }
    }
}
