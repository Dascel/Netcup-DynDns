using NetcupApi.Common;
using NetcupApi.Extentions;
using NetcupApi.Interfaces;
using Newtonsoft.Json;

namespace NetcupApi.Netcup;

public class Api : INetcupApi
{
    private const string ApiEndpoint = "https://ccp.netcup.net/run/webservice/servers/endpoint.php?JSON";
    private readonly HttpClient _httpClient;


    public Api(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<ResponseMessage<SessionObject>?> LoginAsync(int customerNumber, string apiKey, string apiPassword,
        string clientRequestId = "")
    {
        var request = new ApiRequest("login");
        request.AddApiLoginParams(customerNumber, apiKey, apiPassword, clientRequestId);

        return await HttpClientPostAsync<ResponseMessage<SessionObject>>(request.ToHttpContent());
    }

    public async Task<ResponseMessage<string>?> LogoutAsync(int customerNumber, string apiKey, string apiSessionId,
        string clientRequestId = "")
    {
        var request = new ApiRequest("logout");
        request.AddApiSessionAuth(customerNumber, apiKey, apiSessionId, clientRequestId);

        return await HttpClientPostAsync<ResponseMessage<string>>(request.ToHttpContent());
    }

    public async Task<ResponseMessage<DnsRecordSet>?> InfoDnsRecordsAsync(string domainName, int customerNumber,
        string apiKey, string apiSessionId,
        string clientRequestId = "")
    {
        var request = new ApiRequest("infoDnsRecords");
        request.AddApiSessionAuth(customerNumber, apiKey, apiSessionId, clientRequestId);
        request.Parameters.Add("domainname", domainName);

        return await HttpClientPostAsync<ResponseMessage<DnsRecordSet>>(request.ToHttpContent());
    }

    public async Task<ResponseMessage<DnsRecordSet>?> UpdateDnsRecordsAsync(string domainName, int customerNumber,
        string apiKey, string apiSessionId, DnsRecordSet dnsRecordSet, string clientRequestId = "")
    {
        var request = new ApiRequest("updateDnsRecords");
        request.AddApiSessionAuth(customerNumber, apiKey, apiSessionId, clientRequestId);
        request.Parameters.Add("domainname", domainName);
        request.Parameters.Add("dnsrecordset", dnsRecordSet);

        return await HttpClientPostAsync<ResponseMessage<DnsRecordSet>>(request.ToHttpContent());
    }

    private async Task<T?> HttpClientPostAsync<T>(HttpContent content) where T : class
    {
        var result = await _httpClient.PostAsync(ApiEndpoint, content);
        return JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync()) ?? null;
    }
}