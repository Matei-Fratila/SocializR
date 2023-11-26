using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SocializR.Entities.DTOs.Common
{
    public class UserVM
    {
        public string Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ProfilePhotoId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
