using BowlingCalculator.Domain;
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
        public Game CreateNewGame()
        {
            var gameBuilderService = new GameBuilderService();

            return gameBuilderService.CreateDefaultGame(MAX_PINS,NUMBER_OF_FRAMES);
        }
    }
}
