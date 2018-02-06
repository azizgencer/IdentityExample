using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityExample.Identity
{
    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; }

        public ApplicationRole()
        {
            
        }

        public ApplicationRole(string roleName, string description) : base(roleName)
        {
            Description = description;
        }
    }
}