using System;
using System.ComponentModel.DataAnnotations;

namespace ContactManager.Models
{
    public class Person
    {
        public long ID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }
    }
}
