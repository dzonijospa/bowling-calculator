using BowlingCalculator.API.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingCalculator.API.Services
{
    public class ResponseCreator : IResponseCreator
    {
        private readonly ILogger<ResponseCreator> _logger;
        public ResponseCreator(ILogger<ResponseCreator> logger)
        {
            _logger = logger;
        }

        public ScoresResponse GetScoreResponse(bool gameFinished, Domain.GameScore gameScore)
        {
            var result = new List<string>();
            foreach (Domain.FrameProgress frame in gameScore.FrameProgresses)
            {
                string frameScore = frame.ScoringCompleted ? $"{frame.RunningScore}" : "* ";
                result.Add(frameScore);
            }

            return new ScoresResponse() { GameCompleted = gameFinished, FrameProgressScores = result };

        }
    }
}
