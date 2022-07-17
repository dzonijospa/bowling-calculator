using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BowlingCalculator.API.Models
{
    public class ScoresRequest : IValidatableObject
    {
        [Required]
        [MinLength(1, ErrorMessage = "Request must contain at least one roll"), MaxLength(20, ErrorMessage = "Request can't contain more then 20 rolls")]
        public List<int> PinsDowned { get; set; } = new List<int>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            foreach (int pin in PinsDowned)
            {
                if (!IsValid(pin,out string errorMessage))
                {
                    results.Add(new ValidationResult(errorMessage));
                }
            }
            return results;
        }

        private bool IsValid(int pin, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (pin > 10 || pin < 0)
            {
                errorMessage = $"Pin value must be between 0 and 10 and its value is {pin}";
                return false;
            }
            return true;
        }
    }
}
