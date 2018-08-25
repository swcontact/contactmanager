using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ContactContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Contacts.Any())
            {
                return;   // DB has been seeded
            }
            /* 
            context.Contacts.Add(new Contact()
            {
                FirstName = "Huey",
                LastName = "Zhou",
                Information = "{}"
            });
            context.SaveChanges();
            /* */
        }
    }
}
