using BowlingCalculator.API.Models;
using BowlingCalculator.Domain;

namespace BowlingCalculator.API.Services
{
    public interface IResponseCreator
    {
        ScoresResponse GetScoreResponse(bool gameFinished, GameScore gameScore);
    }
}
