using System.Collections.Generic;

namespace BowlingCalculator.Domain.Services
{
    public class GameScoreService
    {
        /// <summary>
        /// Calculates running total and scores per frame that were or are in play
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public GameScore GetGameScore(Game game)
        {
            var frameScores = new List<FrameProgress>();
            short runningTotal = 0;
            foreach (Frame frame in game.Frames)
            {
                if (!frame.FrameState.FrameScore.HasValue)
                    break;//no subsequent frame will have score

                bool isScoringCompleted = frame.FrameState.IsScoringCompleted();
                if (isScoringCompleted)
                    runningTotal += frame.FrameState.FrameScore.Value;

                frameScores.Add(new FrameProgress(frame.FrameNumber, isScoringCompleted, frame.FrameState.FrameScore.Value, runningTotal));

            }

            return new GameScore(game.RunningTotal,frameScores);
        }
    }
}
