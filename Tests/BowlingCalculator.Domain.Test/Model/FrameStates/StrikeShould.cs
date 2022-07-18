using BowlingCalculator.Domain.FrameStates;
using Xunit;

namespace BowlingCalculator.Domain.Test.FrameStates
{
    public class StrikeShould
    {
        private byte max_pin = 10;

        [Fact]
        public void AlwaysTransitionToNextFrame()
        {
            Strike strike = CreateStrike();

            Assert.True(strike.ShouldTransitionToNextFrame());
        }      

        [Fact]
        public void ApplyFirstBonusRoll()
        {

            Strike strike = CreateStrike();

            byte firstRoll = 1;
            strike.ApplyRoll(firstRoll, max_pin);

            Assert.Equal(firstRoll, strike.FirstBonusRoll.Value);
        }

        [Fact]
        public void ReturnScoringNotCompletedBeforeSecondBonusRoll()
        {
            Strike strike = CreateStrike();

            byte firstRoll = 1;
            strike.ApplyRoll(firstRoll, max_pin);

            Assert.False(strike.IsScoringCompleted());
        }

        [Fact]
        public void ApplySecondBonusRoll()
        {

            Strike strike = CreateStrike();

            byte firstRoll = 1;
            byte secondRoll = 2;
            strike.ApplyRoll(firstRoll, max_pin);
            strike.ApplyRoll(secondRoll, max_pin);

            Assert.Equal(secondRoll, strike.SecondBonusRoll.Value);
        }

        [Fact]
        public void CorrectlyCalculateScoreAfterSecondBonusRoll()
        {
            Strike strike = CreateStrike();

            byte firstRoll = 1;
            byte secondRoll = 2;
            strike.ApplyRoll(firstRoll, max_pin);
            strike.ApplyRoll(secondRoll, max_pin);

            Assert.Equal(strike.FirstRoll.Value + strike.FirstBonusRoll.Value + strike.SecondBonusRoll.Value, strike.FrameScore.Value);
        }

        [Fact]
        public void ReturnScoringCompletedAfterSecondBonusRoll()
        {
            Strike strike = CreateStrike();

            byte firstRoll = 1;
            byte secondRoll = 2;
            strike.ApplyRoll(firstRoll, max_pin);
            strike.ApplyRoll(secondRoll, max_pin);

            Assert.True(strike.IsScoringCompleted());
        }

        private Strike CreateStrike()
        {
            return new Strike(max_pin,max_pin,null,null);
        }


    }
}
