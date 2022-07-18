using BowlingCalculator.API.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public Task<ScoresResponse> CalculateScoreAsync(ScoresRequest request)
        {
            return Task.Run(() => 
            {
                Domain.Game game = _gameProvider.CreateNewGame();
                foreach (int pin in request.PinsDowned)
                {
                    game.Roll((byte)pin);
                }

                Domain.Services.GameScoreService gameService = _gameProvider.GetGameScoreService();
                Domain.GameScore gameScore = gameService.GetGameScore(game);

                return _responseCreator.GetScoreResponse(game.IsGameCompleted(), gameScore);
            });                         
        }

    }
}
