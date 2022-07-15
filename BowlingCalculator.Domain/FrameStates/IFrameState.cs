using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingCalculator.Domain.FrameStates
{
    public interface IFrameState
    {
        byte? FirstThrow { get; }
        byte? SecondThrow { get; }
        bool ThrowingDoneForFrame { get; }
        bool IsScoreCalculated { get; }
        IFrameState ApplyPinsDowned(byte pinsDowned);
        byte FrameScore { get; }
        byte MaxPins { get; }
    }
}
