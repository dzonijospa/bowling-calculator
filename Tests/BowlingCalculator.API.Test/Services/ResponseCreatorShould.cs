using BowlingCalculator.API.Models;
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
    public class ResponseCreatorShould
    {
        [Fact]
        public void CorrectlyCreateResponseWhenAllFramesAreFinished()
        {
            var logger = new Mock<ILogger<ResponseCreator>>();
            var responseCreator = new ResponseCreator(logger.Object);
            var frameScores = new List<FrameProgress>();
            frameScores.Add(new FrameProgress(1, true, 10, 10));
            frameScores.Add(new FrameProgress(2, true, 10, 20));
            var request =  new GameScore(20, frameScores);
            var expected = new List<string>() { "10", "20" };

            ScoresResponse response = responseCreator.GetScoreResponse(true, request);

            Assert.True(response.GameCompleted);
            Assert.Equal(expected, response.FrameProgressScores);
        }

        [Fact]
        public void CorrectlyCreateResponseWhenThereAreFramesRunning()
        {
            var logger = new Mock<ILogger<ResponseCreator>>();
            var responseCreator = new ResponseCreator(logger.Object);
            var frameScores = new List<FrameProgress>();
            frameScores.Add(new FrameProgress(1, true, 10, 10));
            frameScores.Add(new FrameProgress(2, false, 10, 20));
            var request = new GameScore(20, frameScores);
            var expected = new List<string>() { "10", "*" };

            ScoresResponse response = responseCreator.GetScoreResponse(true, request);

            Assert.True(response.GameCompleted);
            Assert.Equal(expected, response.FrameProgressScores);
        }

    }
}
