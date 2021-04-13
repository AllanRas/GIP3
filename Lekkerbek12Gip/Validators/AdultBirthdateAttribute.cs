using Lekkerbek12Gip.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Validators
{
    public class AdultBirthdateAttribute:ValidationAttribute
    {
    
        protected override ValidationResult IsValid(object value, ValidationContext
        validationContext)
        {
            // ter info : ObjectInstance bevat het te controleren EventSubscription object
            var eventSubscription = (Klant)validationContext.ObjectInstance;
            var birthdate = (DateTime)value;
            if (birthdate > DateTime.Now)
            {
                return new ValidationResult("De geboortedatum kan niet in de toekomst liggen.");
            }
            else if (birthdate > DateTime.Now.AddYears(-18))
            {
                return new ValidationResult("Je moet 18 of ouder zijn");
            }
            return ValidationResult.Success;
        }
    }

}

