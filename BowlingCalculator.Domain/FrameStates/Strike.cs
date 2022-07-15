using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingCalculator.Domain.FrameStates
{
    public class Strike : IFrameState
    {
        private byte MAX_PINS = 10;
        public bool IsFinished { get { return true; } }

        public bool IsScoringCompleted { get; private set; } = false;

        public byte FrameScore { get; private set; }

        private Func<byte, IFrameState> _applyMethod;

        public Strike()
        {
            _applyMethod = WaitingForFirstThrow;
        }

        public IFrameState ApplyPinsDowned(byte pinsDowned)
        {
            return _applyMethod(pinsDowned);
        }

        private IFrameState WaitingForFirstThrow(byte pinsDown)
        {            
            FrameScore += pinsDown;
            _applyMethod = WaitingForSecondThrow;

            return this;
        }

        private IFrameState WaitingForSecondThrow(byte pinsDown)
        {
            FrameScore += pinsDown;
            IsScoringCompleted = true;
            return this;
        }
    }
}
