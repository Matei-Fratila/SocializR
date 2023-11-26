using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocializR.Entities.DTOs.Interest
{
    public class EditInterestVM
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string ParentId { get; set; }

        public List<SelectListItem> Interests;
    }
}
