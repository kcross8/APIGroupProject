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
            return View();
        }
        public IActionResult Search(string title)
        {
            List<Recipe> recipes = RD.SearchRecipes(title);
            return View("SearchResults", recipes);
        }

        public IActionResult SearchResults(List<Recipe> recipes)
        {
            return View(recipes);
        }
    }
}
