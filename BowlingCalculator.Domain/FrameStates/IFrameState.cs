using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingCalculator.Domain.FrameStates
{
    public interface IFrameState
    {
        bool IsFinished { get; }
        bool IsScoringCompleted { get; }
        IFrameState ApplyPinsDowned(byte pinsDowned);

        byte FrameScore { get; }
    }
}
