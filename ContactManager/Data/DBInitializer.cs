using System.Linq;

namespace ContactManager.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ContactContext context)
        {
            context.Database.EnsureCreated();

            if (context.Contacts.Any())
            {
                return;   // DB has been seeded
            }
            /*
            context.Contacts.Add(new Person()
            {
                FirstName = "Huey",
                LastName = "Zhou",
                Category = Person.Customer,
                Contact = "myemail@email.com",
                Email = "myemail@email.com",
                Birthday = "",
                Telephone = "",
            });
            context.SaveChanges();
            /* */
        }
    }
}
