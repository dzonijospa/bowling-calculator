using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingCalculator.Domain.FrameStates
{
    public class OpenFrame : IFrameState
    {

        private byte MAX_PINS = 10;

        private byte _firstThrow;
        private byte _secondThrow;

        private Func<byte, IFrameState> _applyMethod;

        public bool IsFinished { get; private set; }

        public bool IsScoringCompleted { get; private set; } = false;

        public byte FrameScore { get; private set; }

        public OpenFrame()
        {
            _applyMethod = WaitingForFirstThrow;
        }

        public IFrameState ApplyPinsDowned(byte pinsDowned)
        {
            return _applyMethod(pinsDowned);
        }

        private IFrameState WaitingForFirstThrow(byte pinsDown)
        {
            _firstThrow = pinsDown;
            

            if (_firstThrow == MAX_PINS)
                return new Strike();

            FrameScore += _firstThrow;
            _applyMethod = WaitingForSecondThrow;

            return this;
        }

        private IFrameState WaitingForSecondThrow(byte pinsDown)
        {
            _secondThrow = pinsDown;            

            if (_firstThrow + _secondThrow == MAX_PINS)
                return new Spare(_firstThrow, _secondThrow);

            FrameScore += _secondThrow;
            IsFinished = true;
            IsScoringCompleted = true;

            return this;
        }
    }
}
