using Common.Interfaces;
using System;
using System.Collections.Generic;

namespace SocializR.Entities
{
    public partial class UserRole : IEntity
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public Role Role { get; set; }
        public User User { get; set; }
    }
}
