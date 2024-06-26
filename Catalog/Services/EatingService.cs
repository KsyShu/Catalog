using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace Catalog.Services
{
    public class Eating
    {
        public int? id { get; set; }
        public string name { get; set; }
        //public byte[] photo { get; set; }
        public string photoPath { get; set; }
        public string description { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public static IBuffer EatingImageBuffer { get; set; }
    }
    public class EatingService
    {
        public static async Task<ObservableCollection<Eating>> getAll()
        {
            var result = await App.HttpClient.GetAsync("http://localhost:8080/eating/list");
            result.EnsureSuccessStatusCode();
            var body = await result.Content.ReadAsStringAsync();
            var eatings = JsonConvert.DeserializeObject<ObservableCollection<Eating>>(body);
            return eatings;
        }
        public static async Task Add(Eating eating)
        {
            var body = JsonConvert.SerializeObject(eating);
            var content = new MultipartContent();
            var httpContent = new StringContent(body, Encoding.UTF8, "application/json");
            content.Add(httpContent);
            var result = await App.HttpClient.PostAsync("http://localhost:8080/eating/add", content);
        }
        public static async Task Update(Eating eating)
        {
            var body = JsonConvert.SerializeObject(eating);
            var content = new MultipartContent();
            var httpContent = new StringContent(body, Encoding.UTF8, "application/json");
            content.Add(httpContent);
            var result = await App.HttpClient.PostAsync("http://localhost:8080/eating/update", content);
        }
        public static async Task Delete(Eating eating)
        {
            var result = await App.HttpClient.GetAsync($"http://localhost:8080/eating/delete/{eating.id}");
            result.EnsureSuccessStatusCode();
        }
    }
}