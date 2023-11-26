using Common.Interfaces;
using System;
using System.Collections.Generic;

namespace SocializR.Entities
{
    public partial class RolePermission : IEntity
    {
        public int Id { get; set; }
        public int PermissionId { get; set; }

        public Permission Permission { get; set; }
        public Role Role { get; set; }
    }
}
