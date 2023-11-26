using Common.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocializR.Entities.DTOs.Account
{
    public class RegisterVM : IValidatableObject
    {
        [EmailAddress(ErrorMessage = "Adresa nu este valida!")]
        [MaxLength(100, ErrorMessage = "Campul trebuie sa aiba maximum 100 de caracatere")]
        [Required(ErrorMessage = "Campul este obligatoriu!")]
        //[Remote(action: "IsEmailAvailable", controller: "Account", ErrorMessage = "Email-ul exista deja")]
        public string Email { get; set; }

        [MaxLength(100, ErrorMessage = "Campul trebuie sa aiba maximum 100 de caracatere")]
        [Required(ErrorMessage = "Campul este obligatoriu!")]
        public string FirstName { get; set; }

        [MaxLength(100, ErrorMessage = "Campul trebuie sa aiba maximum 100 de caracatere")]
        [Required(ErrorMessage = "Campul este obligatoriu!")]
        public string LastName { get; set; }

        [MaxLength(100, ErrorMessage = "Campul trebuie sa aiba maximum 100 de caracatere")]
        [Required(ErrorMessage = "Campul este obligatoriu!")]
        public string Password { get; set; }

        public string CityId { get; set; }
        public List<SelectListItem> Cities { get; set; }

        public string CountyId { get; set; }
        public List<SelectListItem> Counties { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        public int Gender { get; set; }

        public bool IsPrivate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var result = new List<ValidationResult>();

            var service = validationContext.GetService(typeof(IValidationService)) as IValidationService;
            var emailExists = service.EmailExists(Email);

            if (emailExists)
            {
                result.Add(new ValidationResult("Email-ul exista deja", new List<string> { nameof(Email) }));
            }

            return result;
        }
    }
}
