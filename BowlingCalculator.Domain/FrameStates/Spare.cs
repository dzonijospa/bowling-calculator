using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingCalculator.Domain.FrameStates
{
    public class Spare : IFrameState
    {
        public bool ThrowingDoneForFrame { get; }

        public bool IsScoreCalculated { get; private set; }

        public byte FrameScore { get; private set; }

        public byte? FirstThrow { get; }

        public byte? SecondThrow { get; }

        public byte MaxPins { get; }

        public Spare(byte maxPins,byte? firstThrow, byte? secondThrow,bool isScoringCompleted)
        {
            MaxPins = maxPins;
            FirstThrow = firstThrow;
            SecondThrow = secondThrow;
            FrameScore = maxPins;
            IsScoreCalculated = isScoringCompleted;
            ThrowingDoneForFrame = true;
        }

        public IFrameState ApplyPinsDowned(byte pinsDowned)
        {
            //bonus roll
            FrameScore = (byte)(MaxPins + pinsDowned); //points are calulated when bonus roll is done
            IsScoreCalculated = true;
            return this;
        }

        public static Spare CreateDefaultSpare(byte maxPins,byte? firstThrow, byte? secondThrow)
        {
            return new Spare(maxPins:maxPins, firstThrow: firstThrow, secondThrow: secondThrow,isScoringCompleted:false);
        }

    }
}
