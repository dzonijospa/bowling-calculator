using System.Collections.Generic;

namespace BowlingCalculator.Domain
{
    public class GameScore
    {
       public short GameRunningTotal { get; }

       public List<FrameProgress> FrameProgresses { get; }

        public GameScore(short gameRunningTotal, List<FrameProgress> frameProgresses)
        {
            GameRunningTotal = gameRunningTotal;
            FrameProgresses = frameProgresses;
        }
    }

    public class FrameProgress
    {
        public byte FrameNumber { get; }

        public bool ScoringCompleted { get; }

        public byte Score { get; }

        public short RunningScore { get; }

        public FrameProgress(byte frameNumber, bool scoringCompleted, byte score, short runningScore)
        {
            FrameNumber = frameNumber;
            ScoringCompleted = scoringCompleted;
            Score = score;
            RunningScore = runningScore;
        }
    }

}