using ElevatorCheckWEBUI.Core.DTO.Maintenance;
using ElevatorCheckWEBUI.Core.DTO.Structure;
using ElevatorCheckWEBUI.Core.Model;
using ElevatorCheckWEBUI.Core.Result;
using ElevatorCheckWEBUI.Helper.SessionHelper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace ElevatorCheckWEBUI.WebUI.Areas.AdminPanel.Controllers
{
    public class MaintenanceController : Controller
    {

        [Area("AdminPanel")]
        public class ProductController : Controller
        {
            private readonly IWebHostEnvironment _webHostEnvironment;

            public ProductController(IWebHostEnvironment webHostEnvironment)
            {
                _webHostEnvironment = webHostEnvironment;
            }

            [HttpGet("/Admin/BakimListesi")]
            public async Task<ActionResult> Index()
            {
                MaintenanceViewModel maintenanceViewModel = new()
                {
                    Maintenances = await GetMaintenances(),
                    Structures = await GetStructures()
                };
                return View(maintenanceViewModel);
            }


            [ValidateAntiForgeryToken]
            [HttpPost("/Admin/BakimEkle")]
            public async Task<IActionResult> AddMaintenance(MaintenanceDTO maintenanceDTO)
            {

                var url = "http://localhost:7275/AddMaintenance";
                var client = new RestClient(url);
                var request = new RestRequest(url, Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + SessionManager.Token);
                var body = JsonConvert.SerializeObject(maintenanceDTO);
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


            [ValidateAntiForgeryToken]
            [HttpPost("/Admin/BakimGuncelle")]
            public async Task<IActionResult> UpdateMaintenance(MaintenanceDTO maintenanceDTO)
            {

                var url = "http://localhost:7275/UpdateMaintenance";
                var client = new RestClient(url);
                var request = new RestRequest(url, Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + SessionManager.Token);
                var body = JsonConvert.SerializeObject(maintenanceDTO);
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


            [HttpGet("/Admin/Urun/{maintenanceGUID}")]
            public async Task<ActionResult> GetStructureDetail(Guid maintenanceGUID)
            {
                var url = "http://localhost:7275/Maintenance/" + maintenanceGUID;
                var client = new RestClient(url);
                var request = new RestRequest(url, Method.Get);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + SessionManager.Token);
                RestResponse response = await client.ExecuteAsync(request);
                var responseObject = JsonConvert.DeserializeObject<ApiResult<MaintenanceDTO>>(response.Content);

                return Json(new { success = true, data = responseObject.Data });
            }


            [ValidateAntiForgeryToken]
            [HttpPost("/Admin/RemoveMaintenance")]
            public async Task<IActionResult> RemoveMaintenance(Guid maintenanceGUID)
            {
                MaintenanceDTO maintenanceDTO = new()
                {
                    Guid = maintenanceGUID
                };

                var url = "http://localhost:7275/RemoveMaintenance/";
                var client = new RestClient(url);
                var request = new RestRequest(url, Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + SessionManager.Token);
                var body = JsonConvert.SerializeObject(maintenanceDTO);
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



            public async Task<List<MaintenanceDTO>> GetMaintenances()
            {
                var url = "http://localhost:7275/Maintenances";
                var client = new RestClient(url);
                var request = new RestRequest(url, Method.Get);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + SessionManager.Token);
                RestResponse response = await client.ExecuteAsync(request);
                var responseObject = JsonConvert.DeserializeObject<ApiResult<List<MaintenanceDTO>>>(response.Content);
                var maintenanceList = responseObject.Data;

                return maintenanceList;
            }
            public async Task<List<StructureDTO>> GetStructures()
            {
                var url = "http://localhost:7275/ActiveStructures";
                var client = new RestClient(url);
                var request = new RestRequest(url, Method.Get);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + SessionManager.Token);
                RestResponse response = await client.ExecuteAsync(request);
                var responseObject = JsonConvert.DeserializeObject<ApiResult<List<StructureDTO>>>(response.Content);
                var structureList = responseObject.Data;

                return structureList;
            }
        }
    }
}
