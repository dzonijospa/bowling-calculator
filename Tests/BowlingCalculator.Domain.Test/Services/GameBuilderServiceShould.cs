using BowlingCalculator.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BowlingCalculator.Domain.Test.Services
{
    public class GameBuilderServiceShould
    {
        [Fact]
        public void CreateDefaultGameBasedOnInputParameters()
        {
            byte maxPins = 10;
            byte numberOfFrames = 10;
            var gameBuilder = new GameBuilder();

            Game game = gameBuilder.CreateDefaultGame(maxPins, numberOfFrames);

            Assert.Equal(maxPins, game.MaxPins);
            Assert.Equal(numberOfFrames, game.Frames.Count);
        }
    }
}
