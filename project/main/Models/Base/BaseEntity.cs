using System.ComponentModel.DataAnnotations.Schema;

namespace main.Models.Base
{
    public class BaseEntity
    {
        [Column("id")]
        public long Id { get; set; }
    }
}
