using ElevatorCheckWEBUI.Core.DTO.Structure;
using ElevatorCheckWEBUI.Core.Result;
using ElevatorCheckWEBUI.Helper.SessionHelper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace ElevatorCheckWEBUI.WebUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class StructureController:Controller
    {
        [HttpGet("/Admin/BinaListesi")]
        public async Task<ActionResult> Index()
        {
            var url = "http://localhost:7275/Structures";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.Token);
            RestResponse response = await client.ExecuteAsync(request);
            var responseObject = JsonConvert.DeserializeObject<ApiResult<List<StructureDTO>>>(response.Content);
            var structureList = responseObject.Data;
            return View(structureList.ToList());
        }


        [ValidateAntiForgeryToken]
        [HttpPost("/Admin/BinaEkle")]
        public async Task<IActionResult> AddCategory(StructureDTO structure)
        {
            var url = "http://localhost:7275/AddStructure";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.Token);
            var body = JsonConvert.SerializeObject(structure);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            var responseObject = JsonConvert.DeserializeObject<ApiResult<StructureDTO>>(response.Content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Json(new { success = true, message = responseObject.Mesaj, data = responseObject.Data });
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return Json(new { success = false, message = responseObject.Mesaj, data = responseObject.HataBilgisi });
            }

            else
            {
                return Json(new { success = false, message = "Hata Oluştu" });
            }

        }


        [ValidateAntiForgeryToken]
        [HttpPost("/Admin/BinaGuncelle")]
        public async Task<IActionResult> UpdateCategory(StructureDTO structure)
        {
            var url = "http://localhost:7275/UpdateStructure";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.Token);
            var body = JsonConvert.SerializeObject(structure);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            var responseObject = JsonConvert.DeserializeObject<ApiResult<bool>>(response.Content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Json(new { success = true, message = responseObject.Mesaj, data = responseObject.Data });
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return Json(new { success = false, message = responseObject.Mesaj, data = responseObject.HataBilgisi });
            }

            else
            {
                return Json(new { success = false, message = "Hata Oluştu" });
            }

        }

        [HttpGet("/Admin/Structure/{structureGUID}")]
        public async Task<ActionResult> GetStructureDetail(Guid guid)
        {
            var url = "http://localhost:7275/Structure/" + guid;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.Token);
            RestResponse response = await client.ExecuteAsync(request);
            var responseObject = JsonConvert.DeserializeObject<ApiResult<StructureDTO>>(response.Content);

            return Json(new { success = true, data = responseObject.Data });
        }
    }
}
