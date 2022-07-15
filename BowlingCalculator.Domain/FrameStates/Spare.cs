using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingCalculator.Domain.FrameStates
{
    public class Spare : IFrameState
    {
        private byte MAX_PINS = 10;

        private readonly int _firstThrow;

        private readonly int _secondThrow;

        public bool IsFinished { get { return true; } }

        public bool IsScoringCompleted { get; private set; } = false;

        public byte FrameScore { get; private set; }

        public Spare(int firstThrow, int secondThrow)
        {
            _firstThrow = firstThrow;
            _secondThrow = secondThrow;
            FrameScore = MAX_PINS;
        }

        public IFrameState ApplyPinsDowned(byte pinsDowned)
        {
            FrameScore += pinsDowned;
            IsScoringCompleted = true;
            return this;
        }

    }
}
