using System;

namespace BowlingCalculator.Domain.Exceptions
{
    [Serializable]
    public class InvalidNumberOfPinsException : DomainException
    {
        public InvalidNumberOfPinsException(string numberOfPins) : base($"Invalid number of pins {numberOfPins}")
        {
        }
    }
}
