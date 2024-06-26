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
    public class Category
    {
        public int? id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int? program { get; set; }
    }
    public class CategoryService
    {
        public static async Task<ObservableCollection<Category>> getAll()
        {
            var result = await App.HttpClient.GetAsync("http://localhost:8080/category/list");
            result.EnsureSuccessStatusCode();
            var body = await result.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<ObservableCollection<Category>>(body);
            return categories;
        }
        public static async Task Add(Category category)
        {
            var body = JsonConvert.SerializeObject(category);

            var httpContent = new StringContent(body, Encoding.UTF8, "application/json");

            var result = await App.HttpClient.PostAsync("http://localhost:8080/category/add", httpContent);

        }
        public static async Task Update(Category category)
        {
            var body = JsonConvert.SerializeObject(category);

            var httpContent = new StringContent(body, Encoding.UTF8, "application/json");

            var result = await App.HttpClient.PostAsync("http://localhost:8080/category/update", httpContent);
        }
        public static async Task Delete(Category category)
        {
            var result = await App.HttpClient.GetAsync($"http://localhost:8080/category/delete/{category.id}");
            result.EnsureSuccessStatusCode();
        }
    }
}
