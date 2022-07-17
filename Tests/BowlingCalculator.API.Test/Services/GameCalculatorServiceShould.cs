using BowlingCalculator.API.Models;
using BowlingCalculator.API.Services;
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
    public class GameCalculatorServiceShould
    {
        [Fact]
        public void ReturnScoreResponse()
        {
            var gameProvider = new Moq.Mock<IGameProvider>();
            gameProvider.Setup(x => x.CreateNewGame()).Returns(new Domain.Game(new LinkedList<Domain.Frame>(), 1, 10, Domain.GameStatus.NotStarted, 0));
            var logger = new Mock<ILogger<GameCalculatorService>>();
            var responseCreator = new Mock<IResponseCreator>();
            responseCreator.Setup(x => x.GetScoreResponse(It.IsAny<bool>(), It.IsAny<Domain.GameScore>())).Returns(new ScoresResponse());

            var calculatorService = new GameCalculatorService(gameProvider.Object,responseCreator.Object, logger.Object);

            var response = calculatorService.CalculateScore(new ScoresRequest());

            Assert.IsType<ScoresResponse>(response);
        }

    }
}
