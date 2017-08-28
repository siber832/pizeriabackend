using System;
using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Database
{
    public class Ingredients
    {
        [Key]
        public String IngredientId { set; get; }
        [Required]
        public String Name { set; get; }
        [Required]
        public decimal Price { set; get; }
    }
}
