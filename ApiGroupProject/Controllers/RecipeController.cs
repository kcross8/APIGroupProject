using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIGroupProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace APIGroupProject.Controllers
{
    public class RecipeController : Controller
    {
        private readonly string _configuration;
        private RecipeDAL RD;
        public RecipeController(IConfiguration configuration)
        {
            _configuration = configuration.GetSection("APIKeys")[""];
            RD = new RecipeDAL(configuration);
        }
        public IActionResult Index()
        {
            Recipe r = RD.GetRecipe("Vegetable-Pasta Oven Omelet");
            return View(r);
        }
        public IActionResult Search()
        {
            List<Recipe> recipes = RD.SearchRecipes("onions");
            return View(recipes);
        }
    }
}
