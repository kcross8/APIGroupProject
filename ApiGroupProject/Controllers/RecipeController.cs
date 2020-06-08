using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using APIGroupProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace APIGroupProject.Controllers
{
    public class RecipeController : Controller
    {
        private readonly string _configuration;
        private readonly APIProjectDBContext _context;
        private RecipeDAL RD;
       
        public RecipeController(IConfiguration configuration, APIProjectDBContext context)
        {
            _configuration = configuration.GetSection("APIKeys")[""];
            RD = new RecipeDAL(configuration);
            _context = context;
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
        public IActionResult Favorites()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var thisUsersFavorites = _context.Favorites.Where(x => x.UserId == id).ToList();
            return View(thisUsersFavorites);
        }

        public IActionResult AddFavorite(string title, string ingredients, string href, string thumbnail)
        {
            Favorites newFavorite = new Favorites();
            if (_context.Favorites.Any(t => t.title == title))
            {
                return RedirectToAction("Index");
            }
            else if (ModelState.IsValid)
            {
                newFavorite.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                newFavorite.title = title;
                newFavorite.ingredients = ingredients;
                newFavorite.href = href;
                newFavorite.thumbnail = thumbnail;
                _context.Favorites.Add(newFavorite);
                _context.SaveChanges();
                return RedirectToAction("Favorites");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public IActionResult RemoveFavorite(string title)
        {
            Favorites found = _context.Favorites.Find(title);
            if (found != null)
            {
                _context.Favorites.Remove(found);
                _context.SaveChanges();
            }
            return RedirectToAction("Favorites");
        }
    }
}
