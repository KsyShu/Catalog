using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Services
{
    class ProfileResult 
    {
        public Employee UserEmployee { get; set; }
        public Client UserClient { get; set; }
    }
    public class Credentials
    {
        public string username { get; set; }
        public string password { get; set; }
    }
    internal class Auth
    {
        public static Employee UserEmployee;
        public static Client UserClient;
        public static async Task<bool> SignIn(string username, string password)
        {
            var credentials = new Credentials
            {
                username = username,
                password = password
            };
            var body = JsonConvert.SerializeObject(credentials);
            var httpContent = new StringContent(body,Encoding.UTF8, "application/json");

            var result = await App.HttpClient.PostAsync("http://localhost:8080/auth", httpContent);
            result.EnsureSuccessStatusCode();
            var resultBody = await result.Content.ReadAsStringAsync();
            var profiles = JsonConvert.DeserializeObject<ProfileResult>(resultBody);
            if (profiles.UserEmployee != null) {
               UserEmployee = profiles.UserEmployee;
                return true;
            }
            if (profiles.UserClient != null)
            {
                UserClient = profiles.UserClient;
                return false;
            }
            throw new Exception("Unauthorized");
        }
    }
}
