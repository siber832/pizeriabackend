using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Pizzeria.Database
{
    public class User 
    {
        [Key]
        public String UserId { get; set; }
        [MaxLength(50),Required]
        public String Loguin { get; set; }
        [MaxLength(50), Required]
        public String Password { get; set; }
        [MaxLength(50), Required]
        public String Name { get; set; }
        [MaxLength(50), Required]
        public String Surnames { get; set; }
        [MaxLength(100)]
        public String Email { get; set; }
        
    }
}

