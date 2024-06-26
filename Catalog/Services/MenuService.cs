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
    public class Menu
    {
        public int? id { get; set; }
        public string name { get; set; }
        public int? eating_id { get; set; }
    }
    public class MenuService
    {
        public static async Task<ObservableCollection<Menu>> getAll()
        {
            var result = await App.HttpClient.GetAsync("http://localhost:8080/menu/list");
            result.EnsureSuccessStatusCode();
            var body = await result.Content.ReadAsStringAsync();
            var menus = JsonConvert.DeserializeObject<ObservableCollection<Menu>>(body);
            return menus;
        }

        public static async Task Add(Menu menu)
        {
            var body = JsonConvert.SerializeObject(menu);

            var httpContent = new StringContent(body, Encoding.UTF8, "application/json");

            var result = await App.HttpClient.PostAsync("http://localhost:8080/menu/add", httpContent);

        }

        public static async Task Update(Menu menu)
        {
            var body = JsonConvert.SerializeObject(menu);

            var httpContent = new StringContent(body, Encoding.UTF8, "application/json");

            var result = await App.HttpClient.PostAsync("http://localhost:8080/menu/update", httpContent);
        }

        public static async Task Delete(Menu menu)
        {
            var result = await App.HttpClient.GetAsync($"http://localhost:8080/menu/delete/{menu.id}");
            result.EnsureSuccessStatusCode();
        }
    }
}

