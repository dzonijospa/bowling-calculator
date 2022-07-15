using BowlingCalculator.Domain.FrameStates;
using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingCalculator.Domain
{
    public class Frame
    {

        private IFrameState _frameState;

        public byte FrameScore { get { return _frameState.FrameScore; } }

        public bool FrameScoreCalculated { get { return _frameState.IsScoringCompleted; } }

        public bool FrameCompleted { get { return _frameState.IsFinished; } }

        public byte FrameNumber { get; }

        public Frame(byte frameNumber)
        {
            FrameNumber = frameNumber;
        }

        public void ApplyPinsDowned(byte pinsDowned)
        {
            IFrameState frameState = _frameState.ApplyPinsDowned(pinsDowned);
            _frameState = frameState;            
        }

    }
}
