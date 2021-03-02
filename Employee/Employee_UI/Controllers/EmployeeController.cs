using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Employee_API;
using Employee_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Employee_UI.Controllers
{
    public class EmployeeController : Controller
    {
        IConfiguration _configuration;

        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            List<MasterEmployee> Employee = new List<MasterEmployee>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("ApiKey", _configuration.GetValue<string>("IDSettings:PublicKey"));
                using (var response = await httpClient.GetAsync(_configuration.GetValue<string>("IDSettings:WebApiBaseUrl") + "MasterEmployees"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Employee = JsonConvert.DeserializeObject<List<MasterEmployee>>(apiResponse);
                }
            }
            return View(Employee);
        }
        public async Task<IActionResult> Entry(int? id)
        {
            
            if (id == null)
            {
                ViewBag.Mode = "C";
                ViewBag.PageName = "Employee";
                return View();
            }
            MasterEmployee Employees = new MasterEmployee();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("ApiKey", _configuration.GetValue<string>("IDSettings:PublicKey"));
                using (var response = await httpClient.GetAsync(_configuration.GetValue<string>("IDSettings:WebApiBaseUrl") + "MasterEmployees/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Employees = JsonConvert.DeserializeObject<MasterEmployee>(apiResponse);
                }
            }
       
            return View(Employees);
        }
        public async Task<IActionResult> Update([FromBody] MasterEmployee masterEmployee)
        {
            var status = "";
            //List<MasterEmployee> employee = new List<MasterEmployee>();
            //using (var httpClient = new HttpClient())
            //{
            //    httpClient.DefaultRequestHeaders.Add("ApiKey", _configuration.GetValue<string>("IDSettings:PublicKey"));
            //    using (var response = await httpClient.GetAsync(_configuration.GetValue<string>("IDSettings:WebApiBaseUrl") + "MasterEmployees/Phone/" + masterEmployee.EmployeePhonenumber))
            //    {
            //        string apiResponse = await response.Content.ReadAsStringAsync();
            //        employee = JsonConvert.DeserializeObject<List<MasterEmployee>>(apiResponse);
            //    }
            //}
            MasterEmployee updatedEmployee = new MasterEmployee();
            using (var httpClient = new HttpClient())
            {
                Uri uri = new Uri(_configuration.GetValue<string>("IDSettings:WebApiBaseUrl") + "MasterEmployees/" + masterEmployee.PkempId);
                StringContent content = new StringContent(JsonConvert.SerializeObject(masterEmployee), Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Add("AppName", _configuration.GetValue<string>("IDSettings:AppName"));
                httpClient.DefaultRequestHeaders.Add("AppKey", _configuration.GetValue<string>("IDSettings:AppKey"));
                httpClient.DefaultRequestHeaders.Add("ClientKey", _configuration.GetValue<string>("IDSettings:ClientKey"));
                using (var response = await httpClient.PutAsync(uri, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        updatedEmployee = JsonConvert.DeserializeObject<MasterEmployee>(apiResponse);
                        status = "success";
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        status = "error";
                    }
                }
            }
            return Json(new { status });
        }
        public async Task<IActionResult> Delete([FromBody] MasterEmployee masterEmployee)
        {
            var status = "";
            using (var httpClient = new HttpClient())
            {
                Uri uri = new Uri(_configuration.GetValue<string>("IDSettings:WebApiBaseUrl") + "MasterEmployees/" + masterEmployee.PkempId);
                MasterEmployee deletedEmployee = new MasterEmployee();
                httpClient.DefaultRequestHeaders.Add("ApiKey", _configuration.GetValue<string>("IDSettings:PublicKey"));
                using (var response = await httpClient.DeleteAsync(uri))
                {
                   
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        deletedEmployee = JsonConvert.DeserializeObject<MasterEmployee>(apiResponse);
                        status = "success";
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        status = "error";
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Save([FromBody] MasterEmployee masterEmployee)
        {
            var status = "";
            List<MasterEmployee> employee = new List<MasterEmployee>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("ApiKey", _configuration.GetValue<string>("IDSettings:PublicKey"));
                using (var response = await httpClient.GetAsync(_configuration.GetValue<string>("IDSettings:WebApiBaseUrl") + "MasterEmployees/Phone/" + masterEmployee.EmployeePhonenumber))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    employee = JsonConvert.DeserializeObject<List<MasterEmployee>>(apiResponse);
                }
            }
            if (employee.Count == 0)
            {
                MasterEmployee createdEmployee = new MasterEmployee();
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(masterEmployee), Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Add("Staging", _configuration.GetValue<string>("IDSettings:PublicKey"));
                    httpClient.DefaultRequestHeaders.Add("ApiKey", "clientspecifickey");
                    using (var response = await httpClient.PostAsync((_configuration.GetValue<string>("IDSettings:WebApiBaseUrl") + "MasterEmployees/"), content))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            createdEmployee = JsonConvert.DeserializeObject<MasterEmployee>(apiResponse);
                            status = "success";
                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                        {
                            status = "error";
                        }
                    }
                }
            }
            else
            {
                status = "Exist contact No";
            }


            return Json(new { status = status });
        }
    }
}

