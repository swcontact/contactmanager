using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.Data
{
    public static class DbInitializer
    {
        public static void Initialize(PersonContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Persons.Any())
            {
                return;   // DB has been seeded
            }
            /*
            context.Persons.Add(new Person()
            {
                FirstName = "Huey",
                LastName = "Zhou",
                Birthday = new DateTime(1999, 1, 2),
                Email = "hueuzhou@hotmail.com",
                Telephone = "6479292569"
            });
            context.SaveChanges();
            */
        }
    }
}
