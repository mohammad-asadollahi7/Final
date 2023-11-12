using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;

namespace Endpoint.MVC.Controllers;


public class BaseController : Controller
{

    private readonly IHttpClientFactory _httpClientFactory;
    public string BaseUrl { get; } = "http://localhost:5153/api/";

    public BaseController(IHttpClientFactory httpClientFactory) =>
                                                 _httpClientFactory = httpClientFactory;


    public async Task<HttpResponseMessage> SendGetRequest(string uri,
                                                          CancellationToken cancellationToken)
    {
        var requestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(BaseUrl + uri)
        };
        return await SendRequest(requestMessage, cancellationToken);
    }


    public async Task<HttpResponseMessage> SendDeleteRequest(string uri,
                                                             CancellationToken cancellationToken)
    {
        var requestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Delete,
            RequestUri = new Uri(BaseUrl + uri)
        };
        return await SendRequest(requestMessage, cancellationToken);
    }


    public async Task<HttpResponseMessage> SendPatchRequest(string uri,
                                                            CancellationToken cancellationToken)
    {
        var requestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Patch,
            RequestUri = new Uri(BaseUrl + uri)
        };
        return await SendRequest(requestMessage, cancellationToken);
    }


    public async Task<HttpResponseMessage> SendPutRequest(string uri,
                                                          string content,
                                                          CancellationToken cancellationToken)
    {
        var requestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Put,
            Content = new StringContent(content, Encoding.UTF8, "application/json"),
            RequestUri = new Uri(BaseUrl + uri)
        };
        return await SendRequest(requestMessage, cancellationToken);
    }



    public async Task<HttpResponseMessage> SendPostRequest(string uri,
                                                           string content,
                                                           CancellationToken cancellationToken)
    {
        var requestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            Content = new StringContent(content, Encoding.UTF8, "application/json"),
            RequestUri = new Uri(BaseUrl + uri)
        };
        return await SendRequest(requestMessage, cancellationToken);
    }

    


    private async Task<HttpResponseMessage> SendRequest(HttpRequestMessage requestMessage,
                                          CancellationToken cancellationToken)
    {
        var jwtToken = Request.Cookies["authorize"];
        if (jwtToken != null)
            requestMessage.Headers.Add("Authorization", "Bearer " + jwtToken);

        var httpClient = _httpClientFactory.CreateClient("EShopClient");
        return await httpClient.SendAsync(requestMessage, cancellationToken);
    }
}
