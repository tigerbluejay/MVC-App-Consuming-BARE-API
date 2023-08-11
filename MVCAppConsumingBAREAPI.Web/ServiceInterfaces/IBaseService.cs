using MVCAppConsumingBAREAPI.Models.Models;

namespace MVCAppConsumingBAREAPI.Web.ServiceInterfaces
{
    public interface IBaseService
    {
        APIResponse responseModel {  get; set; }

        // this will be used to call the API
        // every time we call the API, we'll pass an APIRequest object
        // and return a generic object
        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
