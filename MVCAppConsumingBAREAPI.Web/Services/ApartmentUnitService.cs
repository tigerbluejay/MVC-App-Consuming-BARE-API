using MVCAppConsumingBAREAPI.Models.DTOs;
using MVCAppConsumingBAREAPI.Models.Models;
using MVCAppConsumingBAREAPI.Web.ServiceInterfaces;
using MVCAppConsumingBAREAPI.Models;

namespace MVCAppConsumingBAREAPI.Web.Services
{
    public class ApartmentUnitService : BaseService, IApartmentUnitService
    {
        public readonly IHttpClientFactory _httpClientFactory;
        private string apartmentUnitBaseUrl;
        public ApartmentUnitService(IHttpClientFactory httpClientfactory, IConfiguration configuration) 
            : base(httpClientfactory)
        {
            _httpClientFactory = httpClientfactory;
            apartmentUnitBaseUrl = configuration.GetValue<string>("ServiceUrls:BAREAPI");
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            APIRequest apiRequest = new APIRequest() { 
                ApiType = StaticDetails.ApiType.GET,
                Url = apartmentUnitBaseUrl + "api/v1/ApartmentUnitAPI",
                // the magic string above is defined in the ApartmentUnitController of the API
                Token = token
            };

            return SendAsync<T>(apiRequest);
        }

        public Task<T> GetByIdAsync<T>(int id, string token)
        {
            APIRequest apiRequest = new APIRequest()
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = apartmentUnitBaseUrl + "api/v1/ApartmentUnitAPI/" + id,
                Token = token
            };

            return SendAsync<T>(apiRequest);
        }
        public Task<T> CreateAsync<T>(ApartmentUnitCreateDTO apartmentUnitCreateDTO, string token)
        {
            APIRequest apiRequest = new APIRequest()
            {
                ApiType = StaticDetails.ApiType.POST,
                Url = apartmentUnitBaseUrl + "api/v1/ApartmentUnitAPI",
                Data = apartmentUnitCreateDTO,
                Token = token
            };

            return SendAsync<T>(apiRequest);
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            APIRequest apiRequest = new APIRequest()
            {
                ApiType = StaticDetails.ApiType.DELETE,
                Url = apartmentUnitBaseUrl + "api/v1/ApartmentUnitAPI/" + id,
                Token = token
            };

            return SendAsync<T>(apiRequest);
        }

        public Task<T> UpdateAsync<T>(ApartmentUnitUpdateDTO apartmentUnitUpdateDTO, string token)
        {
            APIRequest apiRequest = new APIRequest()
            {
                ApiType = StaticDetails.ApiType.PUT,
                // here we pass the data
                Data = apartmentUnitUpdateDTO,
                // here we pass the url with the id
                Url = apartmentUnitBaseUrl + "api/v1/ApartmentUnitAPI/" + apartmentUnitUpdateDTO.ApartmentUnitId,
                Token = token
            };

            return SendAsync<T>(apiRequest);
        }
    }
}
