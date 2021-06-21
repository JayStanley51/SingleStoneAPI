using Microsoft.EntityFrameworkCore;
namespace Singlestone.Models
{
    public class ContactContext:DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }
        //public DbSet<CallList> CallLists { get; set; }
    }
}
