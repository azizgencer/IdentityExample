using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityExample.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}