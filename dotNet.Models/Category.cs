using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace dotNet.Models
{
    public class Category
    {
        [Key]
        public int category_id { get; set; }
        [Required]
        [MaxLength(30)]
        public string name { get; set; } = null!;
        [Range(1, 50)]
        public int DisplayOrder { get; set; }


        
       
        
         
    }
}
