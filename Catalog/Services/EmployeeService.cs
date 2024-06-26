using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Catalog;

namespace Catalog.Services
{
    public class Employee
    {
        public int? id { get; set; }
        public string fullname { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
    //class Credentials
    //{
    //    public string username { get; set; }
    //    public string password { get; set; }
    //}
    public class EmployeeService
    {
        public static Employee Profile { get; private set; }
        public static async Task<ObservableCollection<Employee>> getAll()
        {
            var result = await App.HttpClient.GetAsync("http://localhost:8080/employee/list");
            result.EnsureSuccessStatusCode();
            var body = await result.Content.ReadAsStringAsync();
            var employees = JsonConvert.DeserializeObject<ObservableCollection<Employee>>(body);
            return employees;
        }

        public static async Task Add(Employee employee)
        {
            var body = JsonConvert.SerializeObject(employee);
            var httpContent = new StringContent(body, Encoding.UTF8, "application/json");
            var result = await App.HttpClient.PostAsync("http://localhost:8080/employee/add", httpContent);
        }

        public static async Task Update(Employee employee)
        {
            var body = JsonConvert.SerializeObject(employee);
            var httpContent = new StringContent(body, Encoding.UTF8, "application/json");
            var result = await App.HttpClient.PostAsync("http://localhost:8080/employee/update", httpContent);
        }

        public static async Task Delete(Employee employee)
        {
            var result = await App.HttpClient.GetAsync($"http://localhost:8080/employee/delete/{employee.id}");
            result.EnsureSuccessStatusCode();
        }

        //public static async Task<Employee> Login(string username, string password)
        //{
        //    var credentials = new Credentials
        //    {
        //        username = username,
        //        password = password
        //    };
        //    var body = JsonConvert.SerializeObject(credentials);

        //    var httpContent = new StringContent(body, Encoding.UTF8, "application/json");

        //    var result = await App.HttpClient.PostAsync("http://localhost:8080/login", httpContent);
        //    result.EnsureSuccessStatusCode();
        //    var resultBody = await result.Content.ReadAsStringAsync();
        //    var resultEmployee = JsonConvert.DeserializeObject<Employee>(resultBody);

        //    EmployeeService.Profile = resultEmployee;

        //    return resultEmployee;
        //}
        public static void Logout()
        {
            EmployeeService.Profile = null;
        }
    }
}
