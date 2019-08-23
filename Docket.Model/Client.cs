using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docket.Model
{
    public class Client
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(1)]
        public string MiddleInitial { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string Street { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zip")]
        public string Zip { get; set; }

        [RegularExpression(@"^(?!219-09-9999|078-05-1120)(?!666|000|9\d{2})d{3}-(?!00)\d{2}-(?!0{4})\d{4}$", ErrorMessage = "Invalid SSN#")]
        public string SocialSecurity { get; set; }

        public DateTime? BirthDate { get; set; }

        [StringLength(10)]
        public string Phone { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        public int? FavoriteLanguageId { get; set; }

        public ProgrammingLanguage FavoriteLanguage { get; set; }
    }
}
