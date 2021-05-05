using Lekkerbek12Gip.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Validators
{
    public class BestelDateAttribute:ValidationAttribute
    {
                
            protected override ValidationResult IsValid(object value, ValidationContext
           validationContext)
            {
                // ter info : ObjectInstance bevat het te controleren EventSubscription object
                var besteldatum = (Bestelling)validationContext.ObjectInstance;
                var bestelDate = (DateTime)value;
                if (bestelDate < DateTime.Now)
                {
                    return new ValidationResult("De bestelling kan niet in de verleden.");
                }
                else if (bestelDate > DateTime.Now.AddDays(7))
                {
                    return new ValidationResult("Er kan geen bestelling worden geplaatst langer dan 1 week.");
                }
                return ValidationResult.Success;
           
        }

    }
}
