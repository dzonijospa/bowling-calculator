using BowlingCalculator.Domain.FrameStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BowlingCalculator.Domain.Test.FrameStates
{
    public class SpareShould
    {
        private byte max_pin = 10;

        [Fact]
        public void AlwaysTransitionToNextFrame()
        {
            Spare spare = CreateSpare();

            Assert.True(spare.ShouldTransitionToNextFrame());
        }

        [Fact]
        public void NotCalculateScoreBeforeRoll()
        {
            Spare spare = CreateSpare();

            Assert.Null(spare.FrameScore);
        }

        [Fact]
        public void ApplyBonusRoll()
        {
            Spare spare = CreateSpare();

            byte firstRoll = 1;
            spare.ApplyRoll(firstRoll, max_pin);

            Assert.Equal(spare.FirstBonusRoll, firstRoll);
        }

        [Fact]
        public void CalculateScoreAfterBonusRoll()
        {
            Spare spare = CreateSpare();

            byte firstRoll = 1;
            spare.ApplyRoll(firstRoll, max_pin);

            Assert.NotNull(spare.FrameScore);
        }

        [Fact]
        public void CorrectlyCalculateScoreAfterBonusRoll()
        {
            Spare spare = CreateSpare();

            byte firstRoll = 1;
            spare.ApplyRoll(firstRoll, max_pin);

            Assert.Equal(spare.FrameScore.Value, spare.FirstRoll.Value + spare.SecondRoll.Value + spare.FirstBonusRoll.Value);
        }

        private Spare CreateSpare()
        {
            return Spare.CreateDefaultSpare(5,5);
        }
    }
}
