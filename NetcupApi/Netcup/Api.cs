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

    public async Task<ResponseMessage<SessionObject>?> Login(int customerNumber, string apiKey, string apiPassword,
        string clientRequestId = "")
    {
        var request = new ApiRequest("login");
        request.Parameters.Add("customernumber", customerNumber.ToString());
        request.Parameters.Add("apikey", apiKey);
        request.Parameters.Add("apipassword", apiPassword);
        request.Parameters.Add("clientrequestid", clientRequestId);

        return await HttpClientPostAsync<ResponseMessage<SessionObject>>(request.ToHttpContent());
    }

    public async Task<ResponseMessage<string>?> Logout(int customerNumber, string apiKey, string apiSessionId,
        string clientRequestId = "")
    {
        var request = new ApiRequest("logout");
        request.Parameters.Add("customernumber", customerNumber.ToString());
        request.Parameters.Add("apikey", apiKey);
        request.Parameters.Add("apisessionid", apiSessionId);
        request.Parameters.Add("clientrequestid", clientRequestId);

        return await HttpClientPostAsync<ResponseMessage<string>>(request.ToHttpContent());
    }

    public async Task<ResponseMessage<DnsRecordSet>?> InfoDnsRecords(string domainName, int customerNumber,
        string apiKey, string apiSessionId,
        string clientRequestId = "")
    {
        var request = new ApiRequest("infoDnsRecords");
        request.Parameters.Add("domainname", domainName);
        request.Parameters.Add("customernumber", customerNumber.ToString());
        request.Parameters.Add("apikey", apiKey);
        request.Parameters.Add("apisessionid", apiSessionId);
        request.Parameters.Add("clientrequestid", clientRequestId);

        return await HttpClientPostAsync<ResponseMessage<DnsRecordSet>>(request.ToHttpContent());
    }

    public async Task<ResponseMessage<DnsRecordSet>?> UpdateDnsRecords(string domainName, int customerNumber,
        string apiKey, string apiSessionId, DnsRecordSet dnsRecordSet, string clientRequestId = "")
    {
        var request = new ApiRequest("updateDnsRecords");
        request.Parameters.Add("domainname", domainName);
        request.Parameters.Add("customernumber", customerNumber.ToString());
        request.Parameters.Add("apikey", apiKey);
        request.Parameters.Add("apisessionid", apiSessionId);
        request.Parameters.Add("dnsrecordset", dnsRecordSet);
        request.Parameters.Add("clientrequestid", clientRequestId);

        return await HttpClientPostAsync<ResponseMessage<DnsRecordSet>>(request.ToHttpContent());
    }

    private async Task<T?> HttpClientPostAsync<T>(HttpContent content) where T : class
    {
        var result = await _httpClient.PostAsync(ApiEndpoint, content);
        return JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync()) ?? null;
    }
}