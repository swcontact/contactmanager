using System;
using System.ComponentModel.DataAnnotations;

namespace ContactManager.Models
{
    public class PersonaDeleted
    {
        public long ID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(20)]
        public string ContactCategory { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [RegularExpression("[0-9]{7,12}")]
        public string Telephone { get; set; }
    }
}
