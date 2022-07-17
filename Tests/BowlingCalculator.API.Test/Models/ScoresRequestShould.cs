using BowlingCalculator.API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BowlingCalculator.API.Test.Models
{
    public class ScoresRequestShould
    {
        [Fact]
        public void ReturnNotValidWhenRequestContainsElementGreaterThen10()
        {
            var scoresRequest = new ScoresRequest();
            scoresRequest.PinsDowned.Add(1);
            scoresRequest.PinsDowned.Add(11);
            scoresRequest.PinsDowned.Add(12);

            var validationContext = new ValidationContext(scoresRequest);

            var results = scoresRequest.Validate(validationContext);

            Assert.Equal(2,results.Count());
        }
    }
}
