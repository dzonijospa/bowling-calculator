using BowlingCalculator.API.Models;
using BowlingCalculator.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading.Tasks;

namespace BowlingCalculator.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class ScoresController : ControllerBase
    {
        private readonly IGameCalculatorService _gameCalculatorService;
        private readonly ILogger<ScoresController> _logger;

        public ScoresController(IGameCalculatorService gameCalculatorService, ILogger<ScoresController> logger)
        {
            _gameCalculatorService = gameCalculatorService;
            _logger = logger;
        }

        /// <summary>
        /// Calculates game score for 10 pin bowling game
        /// </summary>
        /// <param name="scores">Collection of pins downed per roll</param>
        /// <returns>Progress per frame and is game completed</returns>
        /// /// <remarks>
        /// Sample request:
        ///
        ///     POST /Scores
        ///     {
        ///        "pinsDowned": [1,1,1,1,9,1,2,8,9,1,10,10]   
        ///     }
        ///
        /// Sample response:
        ///
        ///     { 
        ///        “frameProgressScores”: [”2”, ”4”, ”16”, ”35”, ”55”, ”* ”, ”* ”], 
        ///        "gameCompleted": false, 
        ///     }
        /// </remarks>
        ///  <response code="200">Returns the game score</response>
        /// <response code="400">Bad request</response>
        [HttpPost]
        public async Task<ActionResult> ScoresAsync([FromBody] ScoresRequest scores)
        {
            _logger.LogDebug($"request {scores.PinsDowned}");

            ScoresResponse response = await _gameCalculatorService.CalculateScoreAsync(scores);

            _logger.LogDebug($"response {JsonSerializer.Serialize(response)}");

            return Ok(response);
        }
    }
}
