using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BowlingCalculator.Domain.Test.Model
{
    public class GameScoreShould
    {
        [Fact]
        public void ReturnTrueForEqualityOfTwoObjectsIfTheyHaveSameProperties()
        {
            GameScore gameScore1 = CreateGameScore1();
            GameScore gameScore2 = CreateGameScore1();

            Assert.True(gameScore1.Equals(gameScore2));
        }

        [Fact]
        public void ReturnFalseForEqualityOfTwoObjectsIfTheyNotHaveSameProperties()
        {
            GameScore gameScore1 = CreateGameScore1();
            GameScore gameScore2 = CreateGameScore2();

            Assert.False(gameScore1.Equals(gameScore2));
        }

        [Fact]
        public void ReturnSameHashCodeForObjectsIfBothHaveSameProperties()
        {
            GameScore gameScore1 = CreateGameScore1();
            GameScore gameScore2 = CreateGameScore1();

            Assert.Equal(gameScore1.GetHashCode(), gameScore2.GetHashCode());
        }

        [Fact]
        public void ReturnDifferentHashCodeForObjectsThatNotHaveSameProperties()
        {
            GameScore gameScore1 = CreateGameScore1();
            GameScore gameScore2 = CreateGameScore2();

            Assert.NotEqual(gameScore1.GetHashCode(), gameScore2.GetHashCode());
        }

        private GameScore CreateGameScore1()
        {
            var frameProgresses = new List<FrameProgress>();
            frameProgresses.Add(new FrameProgress(1, true, 5, 5));
            frameProgresses.Add(new FrameProgress(2, false, 4, 5));
            GameScore gameScore = new GameScore(5, frameProgresses);
            return gameScore;
        }
        private GameScore CreateGameScore2()
        {
            var frameProgresses = new List<FrameProgress>();
            frameProgresses.Add(new FrameProgress(1, true, 5, 5));
            frameProgresses.Add(new FrameProgress(2, false, 3, 5));
            GameScore gameScore = new GameScore(5, frameProgresses);
            return gameScore;
        }
    }
}
