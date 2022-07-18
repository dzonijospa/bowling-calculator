using System;
using System.Collections.Generic;
using System.Linq;

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

        public override bool Equals(object obj)
        {
            return obj is GameScore score &&
                   GameRunningTotal == score.GameRunningTotal &&
                   score.FrameProgresses.SequenceEqual(FrameProgresses);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(GameRunningTotal, FrameProgresses.Aggregate(0, (x, y) => x.GetHashCode() ^ y.GetHashCode()));
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

        public override bool Equals(object obj)
        {
            return obj is FrameProgress progress &&
                   FrameNumber == progress.FrameNumber &&
                   ScoringCompleted == progress.ScoringCompleted &&
                   Score == progress.Score &&
                   RunningScore == progress.RunningScore;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FrameNumber, ScoringCompleted, Score, RunningScore);
        }
    }

}