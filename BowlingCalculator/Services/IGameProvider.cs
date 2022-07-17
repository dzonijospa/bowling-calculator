using BowlingCalculator.Domain;

namespace BowlingCalculator.API.Services
{
    public interface IGameProvider
    {
        /// <summary>
        /// Create a new game
        /// </summary>
        /// <returns></returns>
        Game CreateNewGame(); 
    }
}
