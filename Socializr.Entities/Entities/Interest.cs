using Common.Interfaces;
using System;
using System.Collections.Generic;

namespace SocializR.Entities
{
    public partial class Interest : IEntity
    {
        public Interest()
        {
            ChildInterests = new HashSet<Interest>();
            UserInterests = new HashSet<UserInterest>();
        }

        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Name { get; set; }

        public Interest Parent { get; set; }
        public ICollection<Interest> ChildInterests { get; set; }
        public ICollection<UserInterest> UserInterests { get; set; }
    }
}
