using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Services
{
    public class Catalog
    {
        public int? id { get; set; }
        //public int? category_id { get; set; }
        public List<Category> category { get; set; }
        public string name { get; set; }
    }
    public class CatalogService
    {
        public static async Task<ObservableCollection<Catalog>> getAll()
        {
            var result = await App.HttpClient.GetAsync("http://localhost:8080/catalog/list");
            result.EnsureSuccessStatusCode();
            var body = await result.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<ObservableCollection<Catalog>>(body);
            return categories;
        }

        public static async Task Add(Catalog catalog)
        {
            var body = JsonConvert.SerializeObject(catalog);

            var httpContent = new StringContent(body, Encoding.UTF8, "application/json");

            var result = await App.HttpClient.PostAsync("http://localhost:8080/catalog/add", httpContent);

        }

        public static async Task Update(Catalog catalog)
        {
            var body = JsonConvert.SerializeObject(catalog);

            var httpContent = new StringContent(body, Encoding.UTF8, "application/json");

            var result = await App.HttpClient.PostAsync("http://localhost:8080/catalog/update", httpContent);
        }

        public static async Task Delete(Catalog catalog)
        {
            var result = await App.HttpClient.GetAsync($"http://localhost:8080/catalog/delete/{catalog.id}");
            result.EnsureSuccessStatusCode();
        }
    }
}
