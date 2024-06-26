using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Catalog;
using System.Net;

namespace Catalog.Services
{
    public class Client
    {
        public int? id { get; set; }
        public string fullname { get; set; }
        public string username { get; set; }
        public int? phone { get; set; }
        public string address { get; set; }
        public string password { get; set; }
        public int? category_id { get; set; }
    }
    class Credentials1
    {
        public string new_login { get; set; }
        public string password { get; set; }
    }
    public class ClientService
    {
        public static Client Profile { get; private set; }

        public static async Task<ObservableCollection<Client>> getAll()
        {
            var result = await App.HttpClient.GetAsync("http://localhost:8080/client/list");
            result.EnsureSuccessStatusCode();
            var body = await result.Content.ReadAsStringAsync();
            var clients = JsonConvert.DeserializeObject<ObservableCollection<Client>>(body);
            return clients;
        }

        public static async Task Add(Client client)
        {
            var body = JsonConvert.SerializeObject(client);
            var httpContent = new StringContent(body, Encoding.UTF8, "application/json");
            var result = await App.HttpClient.PostAsync("http://localhost:8080/client/add", httpContent);
        }

        public static async Task Update(Client client)
        {
            var body = JsonConvert.SerializeObject(client);
            var httpContent = new StringContent(body, Encoding.UTF8, "application/json");
            var result = await App.HttpClient.PostAsync("http://localhost:8080/client/update", httpContent);
        }

        public static async Task Delete(Client client)
        {
            var result = await App.HttpClient.GetAsync($"http://localhost:8080/client/delete/{client.id}");
            result.EnsureSuccessStatusCode();
        }

        //public static async Task<Client> reg_Click(string username, string password)
        //{
        //    var credentials1 = new Credentials1
        //    {
        //        new_login = username,
        //        password = password
        //    };
        //    var body = JsonConvert.SerializeObject(credentials1);

        //    var httpContent = new StringContent(body, Encoding.UTF8, "application/json");

        //    var result = await App.HttpClient.PostAsync("http://localhost:8080/login", httpContent);
        //    result.EnsureSuccessStatusCode();
        //    var resultBody = await result.Content.ReadAsStringAsync();
        //    var resultClient = JsonConvert.DeserializeObject<Client>(resultBody);

        //    ClientService.Profile = resultClient;

        //    return resultClient;
        //}

        public static void Logout()
        {
            ClientService.Profile = null;
        }
    }
}
