using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace dotNet.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int? ProductId { get; set; }

        public Product? Product { get; set; }
        public string? UserId { get; set; }
        public IdentityUser? User { get; set; }

    }
}
