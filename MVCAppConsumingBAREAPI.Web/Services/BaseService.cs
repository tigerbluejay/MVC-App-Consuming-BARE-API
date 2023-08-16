using MVCAppConsumingBAREAPI.Models;
using MVCAppConsumingBAREAPI.Models.Models;
using MVCAppConsumingBAREAPI.Web.ServiceInterfaces;
using Newtonsoft.Json;
using System.Diagnostics;
using System;
using System.Text;
using System.Net.Http.Headers;
using System.Net;

namespace MVCAppConsumingBAREAPI.Web.Services
{
    // base service for all requests to the API
    public class BaseService : IBaseService
    {
        public APIResponse responseModel { get; set; }

        public IHttpClientFactory httpClientFactory { get; set; }

        public BaseService(IHttpClientFactory httpClientfactory)
        {
            responseModel = new APIResponse(); // this is relevant for when we receive the response
            this.httpClientFactory = httpClientfactory; // this is relevant to call the API
        }

        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                /* CONFIGURE THE REQUEST */
                // we create an HttpRequestMessage, and add Headers, RequestUri, Content and Method

                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url); // the url where we'll call the API

                if (apiRequest.Data != null)
                {
                    // we will serialize this data if we are making a POST (Create) or PUT (Update) request
                    /* Serialization is the process of converting an object's state, including its data and structure, 
                     * into a format that can be easily stored, transmitted, or reconstructed. In the context of 
                     * programming and data interchange, serialization is commonly used to convert complex data 
                     * structures, such as objects, into a more portable format like JSON, XML, or binary data. */
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");
                }

                switch (apiRequest.ApiType)
                {
                    case StaticDetails.ApiType.GET:
                        message.Method = HttpMethod.Get;
                        break;
                    case StaticDetails.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case StaticDetails.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    case StaticDetails.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                }

                // we set up the client with a token

                var client = httpClientFactory.CreateClient("BAREAPI");

                if (!string.IsNullOrEmpty(apiRequest.Token))
                {
                    // we pass the token to the client
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", apiRequest.Token);
                }

                /* MAKE THE CALL */
                /* KEY POINT WHERE TO DEBUG */
                HttpResponseMessage httpResponseMessage = null;

                httpResponseMessage = await client.SendAsync(message); // we call the API endpoint and pass the message


                /* PROCESS THE RESPONSE */

                var apiContent = await httpResponseMessage.Content.ReadAsStringAsync();

                try
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(apiContent);

                    bool apiResponseIsBadRequest = false;
                    bool apiResponseIsNotFound = false;

                    bool apiResponseIsNotNull = apiResponse != null;

                    if (apiResponseIsNotNull) { 
                    apiResponseIsBadRequest = apiResponse.StatusCode == HttpStatusCode.BadRequest;
                    apiResponseIsNotFound = apiResponse.StatusCode == HttpStatusCode.NotFound;
					}

					if (apiResponseIsNotNull && (apiResponseIsBadRequest || apiResponseIsNotFound))
                    {

                        apiResponse.StatusCode = HttpStatusCode.BadRequest;
                        apiResponse.IsSuccess = false;
                        var serializedAPIResponse = JsonConvert.SerializeObject(apiResponse);
                        var deserializedResponse = JsonConvert.DeserializeObject<T>(serializedAPIResponse);
                        return deserializedResponse;
                    }

                }
                catch (Exception ex)
                {
                    var deserializedExceptionResponse = JsonConvert.DeserializeObject<T>(apiContent);
                    return deserializedExceptionResponse;
                }

                var deserializedAPIResponse = JsonConvert.DeserializeObject<T>(apiContent);
                return deserializedAPIResponse;

            }
            catch (Exception ex)
            {
                var apiResponse = new APIResponse
                {
                    Errors = new List<string> { Convert.ToString(ex.Message) },
                    IsSuccess = false
                };

                // Serialization converts the object into a JSON-formatted string representation.
                // This string will contain the properties and values of the apiResponse object in JSON format.
                var serializedAPIresponse = JsonConvert.SerializeObject(apiResponse);

                // The deserialization process will attempt to convert the JSON string back into an object of type T.
                var deserializedAPIResponse = JsonConvert.DeserializeObject<T>(serializedAPIresponse);

                return deserializedAPIResponse;

            }
        }
    }
}
