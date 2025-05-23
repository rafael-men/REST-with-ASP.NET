﻿using System.ComponentModel.DataAnnotations.Schema;
using main.Models.Base;

namespace main.Models
{
    [Table("books")]
    public class Book : BaseEntity
    {
        [Column("title")]
        public string Title { get; set; }

        [Column("author")]
        public string Author { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("image")]
        public string Image { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("launch_date")]
        public DateTime LaunchDate { get; set; }
    }
}
