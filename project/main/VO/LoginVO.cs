using System.ComponentModel.DataAnnotations.Schema;

namespace main.VO
{
    public class LoginVO
    {
        [Column("user_name")]
        public string UserName { get; set; }

        [Column("password")]
        public string Password { get; set; }
    }
}
