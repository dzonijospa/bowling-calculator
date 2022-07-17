using System;

namespace BowlingCalculator.Domain.Exceptions
{
    [Serializable]
    public class GameFinishedException : DomainException
    {
        public GameFinishedException() : base($"Game is finished, rolling is not possible!")
        {
        }
    }
}
