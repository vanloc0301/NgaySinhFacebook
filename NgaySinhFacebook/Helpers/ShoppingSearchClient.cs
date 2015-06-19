using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using NgaySinhFacebook.Models;
using System.Configuration;

namespace NgaySinhFacebook.Helpers
{
    public static class ShoppingSearchClient
    {
        private const string SearchApiTemplate = "http://api.shopstyle.com/api/v2/products?pid={0}&cat={1}&offset=0&limit=10";
        private static HttpClient client = new HttpClient();

        public static string AppKey = ConfigurationManager.AppSettings["Search:AppKey"];

        public static Task<SearchResult> GetProductsAsync(string query)
        {
            if (String.IsNullOrEmpty(AppKey))
            {
                throw new InvalidOperationException("Search:AppKey cannot be empty. Make sure you set it in the configuration file.");
            }

            query = query.Replace(" ", "+");
            string searchQuery = String.Format(SearchApiTemplate, AppKey, query);
            var response = client.GetAsync(searchQuery).Result.EnsureSuccessStatusCode();
            return response.Content.ReadAsAsync<SearchResult>();
        }
    }
}