using System.Collections.Generic;

namespace SocializR.Entities.DTOs.Common
{
    public class CurrentUser
    {
        public CurrentUser()
        {

        }
        public CurrentUser(bool isAuthenticated)
        {
            IsAuthenticated = isAuthenticated;
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAuthenticated { get; set; }
        public List<Role> Roles { get; set; }
 
    }
}
