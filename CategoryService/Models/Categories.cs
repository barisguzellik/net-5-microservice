using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CategoryService.Models
{
    [Table("Categories")]
    public class Categories
    {
        [Key]
        [Column("CategoryId")]
        public int category_id { get; set; }
        [Column("Name")]
        public string name { get; set; }
        [Column("Slug")]
        public string slug { get; set; }
        [Column("IsActive")]
        public int is_active { get; set; }
        [Column("Sort")]
        public int sort { get; set; }
    }
}
