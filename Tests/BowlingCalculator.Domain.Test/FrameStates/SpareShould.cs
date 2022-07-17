using BowlingCalculator.Domain.FrameStates;
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
        public void CorrectlyCalculateScoreBeforeBonusRoll()
        {
            Spare spare = CreateSpare();

            Assert.Equal(spare.FrameScore.Value,max_pin);
        }

        [Fact]
        public void ReturnScoringNotCompletedBeforeBonusRoll()
        {
            Spare spare = CreateSpare();

            Assert.False(spare.IsScoringCompleted());
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
        public void ReturnScoringCompletedAfterBonusRoll()
        {
            Spare spare = CreateSpare();

            byte firstRoll = 1;
            spare.ApplyRoll(firstRoll, max_pin);

            Assert.True(spare.IsScoringCompleted());
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
            return new Spare(5, 5, max_pin, null); 
        }
    }
}
