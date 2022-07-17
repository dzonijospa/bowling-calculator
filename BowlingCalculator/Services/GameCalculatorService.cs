using BowlingCalculator.API.Models;
using BowlingCalculator.Domain;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace BowlingCalculator.API.Services
{
    public class GameCalculatorService : IGameCalculatorService
    {
        private readonly IGameProvider _gameProvider;
        private readonly IResponseCreator _responseCreator;
        private readonly ILogger<GameCalculatorService> _logger;

        public GameCalculatorService(IGameProvider gameProvider,IResponseCreator responseCreator, ILogger<GameCalculatorService> logger)
        {
            _gameProvider = gameProvider;
             _responseCreator = responseCreator;
            _logger = logger;
        }

        public ScoresResponse CalculateScore(ScoresRequest request)
        {
            Game game = _gameProvider.CreateNewGame();
            foreach (int pin in request.PinsDowned)
            {
                game.Roll((byte)pin);
            }

            var gameService = new GameService();
            GameScore gameScore = gameService.GetGameScore(game);

            return _responseCreator.GetScoreResponse(game.IsGameCompleted(), gameScore);

        }
        
    }
}
