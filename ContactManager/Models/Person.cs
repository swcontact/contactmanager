using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactManager.Models
{

    public class Person
    {
        public const string Customer = "Customer";
        public const string Supplier = "Supplier";

        public long ID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(20)]
        [CustomCategoryValidator]
        public string Category { get; set; }

        [StringLength(255)]
        public string Contact { get; set; }

        /* not mapped */
        [NotMapped]
        [CustomEmailValidator]
        public string Email { get; set; }

        [NotMapped]
        [CustomBirthdayValidator]
        public string Birthday { get; set; }

        [NotMapped]
        [CustomTelephoneValidator]
        public string Telephone { get; set; }


    }
}
