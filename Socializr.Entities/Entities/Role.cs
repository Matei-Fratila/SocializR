using Common.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace SocializR.Entities
{
    public partial class Role : IdentityRole, IEntity
    {
        public Role()
        {
        }

        public string Description { get; set; }
    }
}
