namespace BowlingCalculator.API.Services
{
    public interface IGameProvider
    {
        /// <summary>
        /// Create a new game
        /// </summary>
        /// <returns></returns>
        Domain.Game CreateNewGame();

        /// <summary>
        /// Get game score service
        /// </summary>
        /// <returns></returns>
        Domain.Services.GameScoreService GetGameScoreService();
    }
}
