using Microsoft.AspNetCore.Mvc;
using MVCAppConsumingBAREAPI.Models.DTOs;
using MVCAppConsumingBAREAPI.Models.Models;
using MVCAppConsumingBAREAPI.Utilities;
using MVCAppConsumingBAREAPI.Web.ServiceInterfaces;
using Newtonsoft.Json;
using System.Diagnostics;
using ErrorViewModel = MVCAppConsumingBAREAPI.Web.Models.ErrorViewModel;

namespace MVCAppConsumingBAREAPI.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApartmentComplexService _apartmentComplexService;

        public HomeController(IApartmentComplexService apartmentComplexService)
        {
            _apartmentComplexService = apartmentComplexService;
        }

        public async Task<IActionResult> Index()
        {
            List<ApartmentComplexDTO> apartmentComplexList = new();

            var response = await _apartmentComplexService.GetAllAsync<APIResponse>
                (HttpContext.Session.GetString(StaticDetails.SessionToken));

            if (response != null && response.IsSuccess)
            {
                apartmentComplexList = 
                    JsonConvert.DeserializeObject<List<ApartmentComplexDTO>>(Convert.ToString(response.Result));
            }

            return View(apartmentComplexList);
        }
		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}