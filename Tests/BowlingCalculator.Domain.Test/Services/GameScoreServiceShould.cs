using BowlingCalculator.Domain.FrameStates;
using BowlingCalculator.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BowlingCalculator.Domain.Test.Services
{
    public class GameScoreServiceShould
    {
        [Fact]
        public void CalculateGameScoreCorrectly()
        {
            var frames = new LinkedList<Frame>();
            frames.AddFirst(new Frame(1, new OpenFrame(5,2,3)));
            frames.AddLast(new Frame(2,new OpenFrame(4,4, null)));
            Game game = new Game(frames, 2, 10, GameStatus.InProgress, 5);
            var frameProgresses = new List<FrameProgress>();
            frameProgresses.Add(new FrameProgress(1, true, 5, 5));
            frameProgresses.Add(new FrameProgress(2, false, 4, 5));
            GameScore gameScoreExpected = new GameScore(game.RunningTotal, frameProgresses);
            var gameScoreService = new GameScoreService();

            GameScore gameScore = gameScoreService.GetGameScore(game);

            Assert.True(gameScoreExpected.Equals(gameScore));
        }
    }
}
