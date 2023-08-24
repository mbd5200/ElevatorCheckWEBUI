using ElevatorCheckWEBUI.Core.DTO.User;
using ElevatorCheckWEBUI.Core.Result;
using ElevatorCheckWEBUI.Helper.SessionHelper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace ElevatorCheckWEBUI.WebUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class UserController:Controller
    {
        [HttpGet("/Admin/Kullanicilar")]
        public async Task<IActionResult> Index()
        {
            var url = "http://localhost:7275/Users";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.Token);
            RestResponse response = await client.ExecuteAsync(request);
            var responseObject = JsonConvert.DeserializeObject<ApiResult<List<UserDTO>>>(response.Content);
            var userList = responseObject.Data;
            return View(userList.ToList());
        }
    }
}
