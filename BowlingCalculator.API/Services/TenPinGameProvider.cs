using BowlingCalculator;
using BowlingCalculator.Domain.Services;
using Microsoft.Extensions.Logging;

namespace BowlingCalculator.API.Services
{
    public class TenPinGameProvider : IGameProvider
    {
        private readonly byte MAX_PINS = 10;
        private readonly byte NUMBER_OF_FRAMES = 10;
        private readonly ILogger<TenPinGameProvider> _logger;
        public TenPinGameProvider(ILogger<TenPinGameProvider> logger)
        {
            _logger = logger;
        }      
        /// <summary>
        /// Creates a new ten pin bowling game
        /// </summary>
        /// <returns></returns>
        public Domain.Game CreateNewGame()
        {
            var gameBuilderService = new Domain.Services.GameBuilder();

            return gameBuilderService.CreateDefaultGame(MAX_PINS,NUMBER_OF_FRAMES);
        }

        public GameScoreService GetGameScoreService()
        {
            var gameScoreService = new GameScoreService();

            return gameScoreService;
        }
    }
}
