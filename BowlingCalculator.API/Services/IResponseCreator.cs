using BowlingCalculator.API.Models;

namespace BowlingCalculator.API.Services
{
    public interface IResponseCreator
    {
        ScoresResponse GetScoreResponse(bool gameFinished, Domain.GameScore gameScore);
    }
}
