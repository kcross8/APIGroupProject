using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APIGroupProject.Models
{
    public class Favorites
    {
        [Key]
        public string title { get; set; }
        public string href { get; set; }
        public string ingredients { get; set; }
        public string thumbnail { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual AspNetUsers User { get; set; }
    }
}
