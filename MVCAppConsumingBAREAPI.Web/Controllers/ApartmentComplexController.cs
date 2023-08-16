using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCAppConsumingBAREAPI.Models.DTOs;
using MVCAppConsumingBAREAPI.Models.Models;
using MVCAppConsumingBAREAPI.Utilities;
using MVCAppConsumingBAREAPI.Web.ServiceInterfaces;
using Newtonsoft.Json;

namespace MVCAppConsumingBAREAPI.Web.Controllers
{
	public class ApartmentComplexController : Controller
	{
		private readonly IApartmentComplexService _apartmentComplexService;
		private readonly IMapper _mapper;

		public ApartmentComplexController(IApartmentComplexService apartmentComplexService, IMapper mapper)
		{
			_apartmentComplexService = apartmentComplexService;
			_mapper = mapper;
		}
		public async Task<IActionResult> Index()
		{
			List<ApartmentComplexDTO> apartmentComplexList = new();

			var response = await _apartmentComplexService.GetAllAsync<APIResponse>(
				HttpContext.Session.GetString(StaticDetails.SessionToken));

			if (response != null && response.IsSuccess)
			{
				apartmentComplexList = JsonConvert.DeserializeObject<List<ApartmentComplexDTO>>(
					Convert.ToString(response.Result));
			}

			return View(apartmentComplexList);
		}

		// CREATE THE APARTMENT COMPLEX
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> CreateApartmentComplex()
		{
			return View("Create");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> CreateApartmentComplex(ApartmentComplexCreateDTO apartmentComplexCreateDTO)
		{
			if (ModelState.IsValid) // Model State refers to all data annotation validations
			{
				var sessionToken = HttpContext.Session.GetString(StaticDetails.SessionToken);
				var response = await _apartmentComplexService.CreateAsync<APIResponse>(
					apartmentComplexCreateDTO, sessionToken);

				if (response != null && response.IsSuccess)
				{
					return RedirectToAction(nameof(Index));
				}

			}
			return View("Create", apartmentComplexCreateDTO); // return to the view with Model state errors
		}

		// DELETE THE APARTMENT COMPLEX
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> DeleteApartmentComplex(int apartmentComplexId)
		{
			// fetch the object to populate the view
			var sessionToken = HttpContext.Session.GetString(StaticDetails.SessionToken);
			var response = await _apartmentComplexService.GetByIdAsync<APIResponse>(
				apartmentComplexId, sessionToken);

			if (response != null && response.IsSuccess)
			{
				ApartmentComplexDTO apartmentComplexDTO = JsonConvert.DeserializeObject<ApartmentComplexDTO>(
					Convert.ToString(response.Result));
				return View("Delete", apartmentComplexDTO);
			}

			return NotFound("Delete");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> DeleteApartmentComplex(ApartmentComplexDTO apartmentComplexDTO)
		{
			var sessionToken = HttpContext.Session.GetString(StaticDetails.SessionToken);
			var response = await _apartmentComplexService.DeleteAsync<APIResponse>(apartmentComplexDTO.Id,
				sessionToken);

			if (response != null && response.IsSuccess)
			{
				return RedirectToAction(nameof(Index));
			}

			return View("Delete", apartmentComplexDTO);
		}

		// UPDATE THE APARTMENT COMPLEX
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> UpdateApartmentComplex(int apartmentComplexId)
		{
			var sessionToken = HttpContext.Session.GetString(StaticDetails.SessionToken);
			var response = await _apartmentComplexService.GetByIdAsync<APIResponse>(apartmentComplexId, sessionToken);

			if (response != null && response.IsSuccess)
			{
				ApartmentComplexDTO apartmentComplexDTO = JsonConvert.DeserializeObject<ApartmentComplexDTO>(
					Convert.ToString(response.Result));
				ApartmentComplexUpdateDTO mappedDTO = _mapper.Map<ApartmentComplexUpdateDTO>(apartmentComplexDTO);
				return View("Update", mappedDTO);
			}

			return NotFound();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> UpdateApartmentComplex(ApartmentComplexUpdateDTO apartmentComplexUpdateDTO)
		{
			if (ModelState.IsValid)
			{
				var sessionToken = HttpContext.Session.GetString(StaticDetails.SessionToken);
				var response = await _apartmentComplexService.UpdateAsync<APIResponse>(apartmentComplexUpdateDTO,
					sessionToken);

				if (response != null && response.IsSuccess)
				{
					return RedirectToAction(nameof(Index));
				}
			}

			return View("Update", apartmentComplexUpdateDTO); // return to the view with validations
		}
	}
}
