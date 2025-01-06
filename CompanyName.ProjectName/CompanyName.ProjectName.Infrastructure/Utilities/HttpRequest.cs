using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Text;
using CompanyName.ProjectName.Infrastructure.Exceptions.BaseExceptions;

namespace CompanyName.ProjectName.Infrastructure.Utilities;

public class HttpRequest : IHttpRequest
{
    private readonly IHttpClientFactory _httpClientFactory;

    public HttpRequest(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<T> GetAsync<T>(string url, Dictionary<string, string> headers = null)
    {
        var httpClient = _httpClientFactory.CreateClient();
        if (headers != null)
        {
            foreach (var header in headers)
                httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
        }

        var response = await httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.UnprocessableEntity ||
                response.StatusCode == HttpStatusCode.BadRequest)
            {

                throw new InvalidInputException();
            }
            else
            {
                throw new InvalidInputException(response.StatusCode);
            }
        }

        return JsonConvert.DeserializeObject<T>(content);
    }

    public async Task<T> PostAsync<T>(string url, object model,
        Dictionary<string, string> headers = null)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var postBody = JsonConvert.SerializeObject(model,
            new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Newtonsoft.Json.Formatting.Indented
            });
        if (headers != null)
        {
            foreach (var header in headers)
                httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
        }

        var response = await httpClient.PostAsync(url,
            new StringContent(postBody, Encoding.UTF8, "application/json"));
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.UnprocessableEntity ||
                response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new InvalidInputException();
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        var result = JsonConvert.DeserializeObject<T>(content);
        return result;
    }

    public async Task<T> PutAsync<T>(string url, object model,
        Dictionary<string, string> headers = null)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var postBody = JsonConvert.SerializeObject(model);
        if (headers != null)
        {
            foreach (var header in headers)
                httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
        }

        var response = await httpClient.PutAsync(url,
            new StringContent(postBody, Encoding.UTF8, "application/json"));
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.UnprocessableEntity ||
                response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new InvalidInputException();
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        var result = JsonConvert.DeserializeObject<T>(content);
        return result;
    }

    public async Task DeleteAsync(string url, Dictionary<string, string> headers = null)
    {
        var httpClient = _httpClientFactory.CreateClient();
        if (headers != null)
        {
            foreach (var header in headers)
                httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
        }

        var response = await httpClient.DeleteAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.UnprocessableEntity ||
                response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new InvalidInputException();
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
    }
}

