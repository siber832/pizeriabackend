using Pizzeria.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzeria.Database
{
    public class Comment
    {
        [Key]
        public String CommentId { set; get; }
        [Required]
        public String Text { set; get; }
        [DefaultValue(0),Required]
        public int Rating { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public String User { get; set; }
        [Required]
        public String Pizza { get; set; }

    }
}
