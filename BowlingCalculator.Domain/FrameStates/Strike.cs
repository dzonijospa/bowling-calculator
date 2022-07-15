using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingCalculator.Domain.FrameStates
{
    public class Strike : IFrameState
    {
        public bool ThrowingDoneForFrame { get { return true; } }

        public bool IsScoreCalculated { get; private set; } = false;

        public byte FrameScore { get; private set; }

        public byte? FirstThrow { get; }

        public byte? SecondThrow { get; }

        public byte? FirstBonusRoll { get; private set; }

        public byte? SecondBonusRoll { get; private set; }

        public byte MaxPins { get; }

        public Strike(byte maxPins,bool isScoringCompleted, byte frameScore, byte? firstThrow, byte? secondThrow, byte? firstBonusRoll, byte? secondBonusRoll)
        {
            MaxPins = maxPins;
            IsScoreCalculated = isScoringCompleted;
            FrameScore = frameScore;
            FirstThrow = firstThrow;
            SecondThrow = secondThrow;
            FirstBonusRoll = firstBonusRoll;
            SecondBonusRoll = secondBonusRoll;
        }

        public IFrameState ApplyPinsDowned(byte pinsDowned)
        {
            if (!FirstBonusRoll.HasValue)
                return ApplyFirstBonusRoll(pinsDowned);
            else
                return ApplySecondBonusRoll(pinsDowned);
        }

        private IFrameState ApplyFirstBonusRoll(byte pinsDown)
        {
            FirstBonusRoll = pinsDown;

            return this;
        }

        private IFrameState ApplySecondBonusRoll(byte pinsDown)
        {
            SecondBonusRoll = pinsDown;
            FrameScore = (byte)(MaxPins + FirstBonusRoll.Value + SecondBonusRoll.Value);
            IsScoreCalculated = true;
            return this;
        }

        public static Strike CreateDefaultStrike(byte maxPins)
        {
            return new Strike(maxPins:maxPins, isScoringCompleted: false, frameScore: default(byte), firstThrow: maxPins,
                                secondThrow: null, firstBonusRoll: null, secondBonusRoll: null);
        }
    }
}
