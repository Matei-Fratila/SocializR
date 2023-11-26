using Common.Interfaces;
using System;
using System.Collections.Generic;

namespace SocializR.Entities
{
    public partial class FriendRequest : IEntity
    {
        public string RequesterUserId { get; set; }
        public string RequestedUserId { get; set; }
        public string RequestMessage { get; set; }
        public DateTime CreatedOn { get; set; }

        public User RequestedUser { get; set; }
        public User RequesterUser { get; set; }
    }
}
