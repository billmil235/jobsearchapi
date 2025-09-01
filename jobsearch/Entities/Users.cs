using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobSearch.Entities
{
    [Table("users")]
    public class Users
    {
        [Key]
        [Column("userid")]
        public Guid UserId { get; set; }

        [Column("username")]
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Column("password")]
        [Required]
        public string Password { get; set; } = string.Empty;

        [Column("firstname")]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Column("lastname")]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Column("emailverified")]
        public bool EMailVerified { get; set; }

        [Column("dateofbirth")]
        public DateOnly DateOfBirth { get; set; }
    }
}