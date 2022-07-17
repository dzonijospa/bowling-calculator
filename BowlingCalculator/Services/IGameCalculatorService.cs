using BowlingCalculator.API.Models;

namespace BowlingCalculator.API.Services
{
    public interface IGameCalculatorService
    {
        /// <summary>
        /// Calculates current game score 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ScoresResponse CalculateScore(ScoresRequest request);
    }
}
