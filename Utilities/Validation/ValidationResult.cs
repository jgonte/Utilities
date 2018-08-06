using System.Collections.Generic;
using System.Linq;

namespace Utilities.Validation
{
    public class ValidationResult
    {
        public List<ValidationError> Errors { get; set; } = new List<ValidationError>();

        public bool IsValid => !Errors.Any();
    }
}
