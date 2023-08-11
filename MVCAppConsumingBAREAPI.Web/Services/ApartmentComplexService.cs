using MVCAppConsumingBAREAPI.Models.DTOs;
using MVCAppConsumingBAREAPI.Models.Models;
using MVCAppConsumingBAREAPI.Web.ServiceInterfaces;

namespace MVCAppConsumingBAREAPI.Web.Services
{
    public class ApartmentComplexService : BaseService, IApartmentComplexService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string apartmentComplexBaseUrl;


        public ApartmentComplexService(IHttpClientFactory httpClientfactory, IConfiguration configuration) 
            : base(httpClientfactory)
        {
            _httpClientFactory = httpClientfactory;
            apartmentComplexBaseUrl = configuration.GetValue<string>("ServiceUrls:BAREAPI");
        }


        public Task<T> GetAllAsync<T>(string token)
        {
            APIRequest apiRequest = new APIRequest()
            {
                ApiType = Utilities.StaticDetails.ApiType.GET,
                Url = apartmentComplexBaseUrl + "/api/v1/ApartmentComplexAPI",
                Token = token
            };

            return SendAsync<T>(apiRequest);
        }

        public Task<T> GetByIdAsync<T>(int id, string token)
        {
            APIRequest apiRequest = new APIRequest()
            {
                ApiType = Utilities.StaticDetails.ApiType.GET,
                Url = apartmentComplexBaseUrl + "/api/v1/ApartmentComplexAPI/" + id,
                Token = token
            };

            return SendAsync<T>(apiRequest);
        }


        public Task<T> CreateAsync<T>(ApartmentComplexCreateDTO apartmentComplexCreateDTO, string token)
        {
            APIRequest apiRequest = new APIRequest()
            {
                ApiType = Utilities.StaticDetails.ApiType.POST,
                Data = apartmentComplexCreateDTO,
                Url = apartmentComplexBaseUrl + "/api/v1/ApartmentComplexAPI",
                Token = token
            };

            return SendAsync<T>(apiRequest);    

        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            APIRequest apiRequest = new APIRequest()
            {
                ApiType = Utilities.StaticDetails.ApiType.DELETE,
                Url = apartmentComplexBaseUrl + "/api/v1/ApartmentComplexAPI/" + id,
                Token = token
            };

            return SendAsync<T>(apiRequest);

        }

        public Task<T> UpdateAsync<T>(ApartmentComplexUpdateDTO apartmentComplexUpdateDTO, string token)
        {
            APIRequest apiRequest = new APIRequest()
            {
                ApiType = Utilities.StaticDetails.ApiType.PUT,
                Data = apartmentComplexUpdateDTO,
                Url = apartmentComplexBaseUrl + "/api/v1/ApartmentComplexAPI/" + apartmentComplexUpdateDTO.Id,
                Token = token
            };

            return SendAsync<T>(apiRequest);
        }
    }
}
