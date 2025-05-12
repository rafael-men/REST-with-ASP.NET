using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace main.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("user_name")]
        public string UserName { get; set; }
        [Column("full_name")]
        public string FullName { get; set; }
        [Column("password")]
        public string Password { get; set; }
    } 
}
