using System.Collections.Generic;

namespace BowlingCalculator.API.Models
{
    public class ScoresResponse
    {
         public List<string> FrameProgressScores { get; set; } 

         public bool GameCompleted { get; set; }

    }
}
