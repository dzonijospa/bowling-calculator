using BowlingCalculator.API.Models;
using System.Threading.Tasks;

namespace BowlingCalculator.API.Services
{
    public interface IGameCalculatorService
    {
        /// <summary>
        /// Calculates current game score 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ScoresResponse> CalculateScoreAsync(ScoresRequest request);
    }
}
