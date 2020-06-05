using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGroupProject.Models
{

    public class RecipeSearch
    {
        public string title { get; set; }
        public float version { get; set; }
        public string href { get; set; }
        public Recipe[] results { get; set; }
    }
    
    public class Recipe
    {
        public string title { get; set; }
        public string href { get; set; }
        public string ingredients { get; set; }
        public string thumbnail { get; set; }
    }

}