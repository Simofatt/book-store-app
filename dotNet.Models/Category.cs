using System.ComponentModel.DataAnnotations;

namespace dotNet.Models
{
    public class Category
    {
        [Key]
        public int category_id { get; set; }
        [Required]
        [MaxLength(10)]
        public string name { get; set; }
        [MaxLength(20)]
        public string description { get; set; }


    }
}
