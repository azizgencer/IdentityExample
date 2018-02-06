using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using IdentityExample.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityExample.DAL
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityContext() : base("MyConnString")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Tablo isimlerinin sonuna 's' takısının eklenmesini engeller.
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}