using MVCAppConsumingBAREAPI.Models.DTOs;

namespace MVCAppConsumingBAREAPI.Web.ServiceInterfaces
{
    public interface IApartmentUnitService
    {
        // we need to pass the token with every call because we want to be able
        // to access the endpoints that require authentication (credentials) and authorization (roles)
        Task<T> GetAllAsync<T>(string token);

        Task<T> GetByIdAsync<T>(int id, string token);

        Task<T> CreateAsync<T>(ApartmentUnitCreateDTO apartmentComplexCreateDTO, string token);

        Task<T> UpdateAsync<T>(ApartmentUnitUpdateDTO apartmentComplexUpdateDTO, string token);

        Task<T> DeleteAsync<T>(int id, string token);
    }
}
