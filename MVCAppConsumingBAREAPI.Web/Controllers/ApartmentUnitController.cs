using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCAppConsumingBAREAPI.Models;
using MVCAppConsumingBAREAPI.Models.DTOs;
using MVCAppConsumingBAREAPI.Models.Models;
using MVCAppConsumingBAREAPI.Models.ViewModels;
using MVCAppConsumingBAREAPI.Web.ServiceInterfaces;
using MVCAppConsumingBAREAPI.Web.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics.Metrics;

namespace MVCAppConsumingBAREAPI.Web.Controllers
{
	public class ApartmentUnitController : Controller
	{
		private readonly IMapper _mapper;
		private readonly IApartmentComplexService _apartmentComplexService;
		private readonly IApartmentUnitService _apartmentUnitService;

		public ApartmentUnitController(IMapper mapper, IApartmentComplexService apartmentComplexService,
			IApartmentUnitService apartmentUnitService)
		{
			_mapper = mapper;
			_apartmentComplexService = apartmentComplexService;
			_apartmentUnitService = apartmentUnitService;
		}

		public async Task<IActionResult> IndexApartmentUnit()
		{
			List<ApartmentUnitDTO> apartmentUnitList = new();

			var response = await _apartmentUnitService.GetAllAsync<APIResponse>(
				HttpContext.Session.GetString(StaticDetails.SessionToken));

			if (response != null && response.IsSuccess)
			{
				apartmentUnitList = JsonConvert.DeserializeObject<List<ApartmentUnitDTO>>(
					Convert.ToString(response.Result));
			}

			return View(apartmentUnitList);

		}

		// CREATE APARTMENT UNIT
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> CreateApartmentUnit()
		{
			// we need to initialize this to populate the dropdown
			// which we will use to display the apartment complex id
			ApartmentUnitCreateVM apartmentUnitCreateVM = new ApartmentUnitCreateVM();

			var response = await _apartmentComplexService.GetAllAsync<APIResponse>(
				HttpContext.Session.GetString(StaticDetails.SessionToken));

			if (response != null && response.IsSuccess)
			{
				apartmentUnitCreateVM.ApartmentComplexList = GenerateList(response);


			}
			return View(apartmentUnitCreateVM);
		}

		private List<SelectListItem> GenerateList(APIResponse response)
		{
			List<ApartmentComplexDTO> complexDTOs = 
				JsonConvert.DeserializeObject<List<ApartmentComplexDTO>>(Convert.ToString(response.Result));

			List<SelectListItem> selectListItems = complexDTOs
				.Select(i => new SelectListItem
				{
					Text = i.ComplexName,
					Value = i.Id.ToString()
				})
				.ToList(); // Ensure the result is a List<SelectListItem>

			return selectListItems;
			/* .Select(i => new SelectListItem { ...}): After deserializing the JSON data into a list of 
			ApartmentComplexDTO objects, the code uses the Select LINQ method to project each 
			ApartmentComplexDTO object into a new SelectListItem object.
			Text = i.ComplexName, Value = i.Id.ToString(): Within the Select method, for each ApartmentComplexDTO
			object(i), a new SelectListItem object is created.The Text property of the SelectListItem is set to 
			the ComplexName property of the ApartmentComplexDTO, and the Value property is set to the string 
			representation of the Id property of the ApartmentComplexDTO *

			/* The looping is actually implicit in the use of the LINQ Select method. The Select method iterates 
			over each element in the source collection (in this case, the list of ApartmentComplexDTO objects) 
			and applies the given transformation logic to create a new collection (in this case, a list of 
			SelectListItem objects).

			So, while there is no explicit for or foreach loop in the code you provided, the Select method 
			internally handles the looping over each ApartmentComplexDTO object to create the corresponding 
			SelectListItem objects. This is one of the powerful features of LINQ, where you can work with collections 
			in a more concise and expressive manner without writing explicit loops. */

		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> CreateApartmentUnit(ApartmentUnitCreateVM apartmentUnitCreateVM)
		{
			var sessionToken = HttpContext.Session.GetString(StaticDetails.SessionToken);

			if (ModelState.IsValid)
			{
				var response = await _apartmentUnitService.CreateAsync<APIResponse>(
					apartmentUnitCreateVM.ApartmentUnit, sessionToken);

				if (response != null && response.IsSuccess)
				{
					return RedirectToAction(nameof(IndexApartmentUnit));
				}
				else if (response.Errors.Count > 0)
				{
					ModelState.AddModelError("Error Messages", response.Errors.FirstOrDefault());
				}
			}

			// the next steps are to populate the dropdown
			// it happens when there are errors and the creation is not successful,
			// we need to reload the page and pass on to it the apartment complex list to try again
			var response2 = await _apartmentComplexService.GetAllAsync<APIResponse>(sessionToken);

			if (response2 != null && response2.IsSuccess)
			{

				apartmentUnitCreateVM.ApartmentComplexList = GenerateList(response2);
			}

			return View(apartmentUnitCreateVM);
		}


		// DELETE APARTMENT UNIT
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> DeleteApartmentUnit(string apartmentUnitId)
		{
			var sessionToken = HttpContext.Session.GetString(StaticDetails.SessionToken);
			ApartmentUnitDeleteVM apartmentUnitDeleteVM = new ApartmentUnitDeleteVM();

			var response = await _apartmentUnitService.GetByIdAsync<APIResponse>(apartmentUnitId, sessionToken);

			if (response != null && response.IsSuccess)
			{
				ApartmentUnitDTO apartmentUnitDTO = JsonConvert.DeserializeObject<ApartmentUnitDTO>(
					Convert.ToString(response.Result));
				apartmentUnitDeleteVM.ApartmentUnit = apartmentUnitDTO;
			}

			// the next steps are to populate the dropdown
			var response2 = await _apartmentComplexService.GetAllAsync<APIResponse>(sessionToken);

			if (response2 != null && response2.IsSuccess)
			{
				apartmentUnitDeleteVM.ApartmentComplexList = GenerateList(response2);

				return View(apartmentUnitDeleteVM);
			}

			return NotFound();

		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> DeleteApartmentUnit(ApartmentUnitDeleteVM apartmentUnitDeleteVM)
		{
			var sessionToken = HttpContext.Session.GetString(StaticDetails.SessionToken);

			var response = await _apartmentUnitService.DeleteAsync<APIResponse>(
				apartmentUnitDeleteVM.ApartmentUnit.ApartmentUnitId, sessionToken);

			if (response != null && response.IsSuccess)
			{
				return RedirectToAction(nameof(IndexApartmentUnit));
			}

			return View(apartmentUnitDeleteVM);
		}

		// UPDATE APARTMENT UNIT
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> UpdateApartmentUnit(string apartmentUnitId)
		{
			var sessionToken = HttpContext.Session.GetString(StaticDetails.SessionToken);

			ApartmentUnitUpdateVM apartmentUnitUpdateVM = new ApartmentUnitUpdateVM();

			// before we update, we retrieve the object
			var response = await _apartmentUnitService.GetByIdAsync<APIResponse>(apartmentUnitId, sessionToken);

			if (response != null && response.IsSuccess)
			{
				// we populate the view model with the object we retrieved
				ApartmentUnitDTO apartmentUnitDTO = JsonConvert.DeserializeObject<ApartmentUnitDTO>(
					Convert.ToString(response.Result));
				apartmentUnitUpdateVM.ApartmentUnit = _mapper.Map<ApartmentUnitUpdateDTO>(apartmentUnitDTO);
			}

			// the next steps are to populate the dropdown
			var response2 = await _apartmentComplexService.GetAllAsync<APIResponse>(sessionToken);

			if (response2 != null && response2.IsSuccess)
			{
				apartmentUnitUpdateVM.ApartmentComplexList = GenerateList(response2);

				return View(apartmentUnitUpdateVM);
			}

			return NotFound();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> UpdateApartmentUnit(ApartmentUnitUpdateVM apartmentUnitUpdateVM)
		{
			var sessionToken = HttpContext.Session.GetString(StaticDetails.SessionToken);

			if (ModelState.IsValid)
			{
				var response = await _apartmentUnitService.UpdateAsync<APIResponse>(
					apartmentUnitUpdateVM.ApartmentUnit, sessionToken);

				if (response != null && response.IsSuccess)
				{
					return RedirectToAction(nameof(IndexApartmentUnit));
				}
				else if (response != null && response.Errors.Count > 0)
				{
					ModelState.AddModelError("Error Messages", response.Errors.FirstOrDefault());
				}
				return View(apartmentUnitUpdateVM);
			}

			// the next steps are to populate the dropdown
			// it happens when there are errors and the creation is not successful,
			// we need to reload the page and pass on to it the apartment complex list to try again
			var response2 = await _apartmentComplexService.GetAllAsync<APIResponse>(sessionToken);

			if (response2 != null && response2.IsSuccess)
			{
				apartmentUnitUpdateVM.ApartmentComplexList = GenerateList(response2);
			}

			return View(apartmentUnitUpdateVM);
		}
	}
}

