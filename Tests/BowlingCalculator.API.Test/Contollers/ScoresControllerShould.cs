using BowlingCalculator.API.Models;
using BowlingCalculator.API.Services;
using BowlingCalculator.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace BowlingCalculator.API.Test.Contollers
{
    public class ScoresControllerShould
    {
        [Fact]
        public void ReturnStatusOkAndValidObjectWhenValidObjectPassed()
        {
            var gameCalculatorService = new Moq.Mock<IGameCalculatorService>();
            var gameServiceResponse = CreateResponse();
            gameCalculatorService.Setup(x => x.CalculateScore(It.IsAny<ScoresRequest>())).Returns(gameServiceResponse);
            var logger = new Mock<ILogger<ScoresController>>();
            var controller = new ScoresController(gameCalculatorService.Object, logger.Object);

            ActionResult response = controller.Scores(new ScoresRequest());
            var result = response as OkObjectResult;

            Assert.NotNull(result);
            Assert.Same(result.Value, gameServiceResponse);

        }

        private ScoresResponse CreateResponse()
        {
            return new ScoresResponse() { GameCompleted = false, FrameProgressScores = new List<string>() { "1", "2" } }; 
        }
    }
}
