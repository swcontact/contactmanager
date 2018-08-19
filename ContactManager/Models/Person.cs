using System;

namespace ContactManager.Models
{
    public class Person
    {
        public long ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
    }
}
