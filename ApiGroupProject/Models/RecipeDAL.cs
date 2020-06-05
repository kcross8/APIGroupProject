using APIGroupProject.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace APIGroupProject.Models
{
    public class RecipeDAL
    {
        private readonly IConfiguration Configuration;
        public RecipeDAL(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public string GetAPIString(string title, bool search)
        {
            string searchType;
            if (search)
            {
                searchType = "q";
            }
            else
            {
                searchType = "q";
            }
            string key = Configuration.GetSection("APIKeys")[""];
            string url = $"http://www.recipepuppy.com/api/?{searchType}={title}";
            HttpWebRequest request = WebRequest.CreateHttp(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader streamReader = new StreamReader(response.GetResponseStream());
            string output = streamReader.ReadToEnd();
            return output;
        }
        public Recipe GetRecipe(string title)
        {
            string recipeData = GetAPIString(title, false);
            JObject json = JObject.Parse(recipeData);
            //This will automatically make the JSON into a Recipe and match the keys with the names in the property
            //They are case sensitive, and know that not all properties in the API are required 
            //IE: A model can contain only 3 properties out of 30 in an API endpoint
            Recipe r = JsonConvert.DeserializeObject<Recipe>(json.ToString());
            return r;
        }
        public List<Recipe> SearchRecipes(string search)
        {
            string output = GetAPIString(search, true);
            JObject json = JObject.Parse(output);
            RecipeSearch results = JsonConvert.DeserializeObject<RecipeSearch>(json.ToString());
            List<Recipe> recipes = results.Search.ToList();
            return recipes;
        }
    }
}
