using System.ComponentModel.DataAnnotations.Schema;

namespace main.VO
{
    public class UserVO
    {
        [Column("user_name")]
        public string UserName { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("full_name")]
        public string FullName { get; set; }
    }
}
