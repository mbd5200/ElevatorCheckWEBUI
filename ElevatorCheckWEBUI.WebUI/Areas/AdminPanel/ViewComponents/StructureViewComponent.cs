using Microsoft.AspNetCore.Mvc;
using ElevatorCheckWEBUI.Core.DTO.Structure;

namespace ElevatorCheckWEBUI.WebUI.Areas.AdminPanel.ViewComponents
{
    public class StructureViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(List<StructureDTO> structures)
        {
            return View("~/Areas/AdminPanel/Views/Shared/Component/Structure/Structures.cshtml", structures);
        }
    }
}