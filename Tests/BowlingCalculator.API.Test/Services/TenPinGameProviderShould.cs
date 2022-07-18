using BowlingCalculator.API.Services;
using BowlingCalculator.Domain;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BowlingCalculator.API.Test.Services
{
    public class TenPinGameProviderShould
    {
        [Fact]
        public void CreateTenPinGame()
        {
            var logger = new Mock<ILogger<TenPinGameProvider>>();
            var gameProvider = new TenPinGameProvider(logger.Object);

            Game game = gameProvider.CreateNewGame();

            Assert.NotNull(game);
            Assert.Equal(10,game.MaxPins);
        }

        [Fact]
        public void ReturnGameScoreService()
        {
            var logger = new Mock<ILogger<TenPinGameProvider>>();
            var gameProvider = new TenPinGameProvider(logger.Object);

            Domain.Services.GameScoreService gameService = gameProvider.GetGameScoreService();

            Assert.NotNull(gameService);
        }
    }
}
