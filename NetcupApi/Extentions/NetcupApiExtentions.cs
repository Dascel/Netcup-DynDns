using System.Text;
using NetcupApi.Common;
using Newtonsoft.Json;

namespace NetcupApi.Extentions;

public static class NetcupApiExtentions
{
    public static HttpContent ToHttpContent(this ApiRequest request)
    {
        return new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
    }

    public static ApiRequest AddApiSessionAuth(this ApiRequest request, int customerNumber, string apiKey, string apiSessionId,
        string clientRequestId)
    {
        request.Parameters.Add("customernumber", customerNumber.ToString());
        request.Parameters.Add("apikey", apiKey);
        request.Parameters.Add("apisessionid", apiSessionId);
        request.Parameters.Add("clientrequestid", clientRequestId);
        
        return request;
    }
    
    public static ApiRequest AddApiLoginParams(this ApiRequest request, int customerNumber, string apiKey, string apiPassword,
        string clientRequestId)
    {
        request.Parameters.Add("customernumber", customerNumber.ToString());
        request.Parameters.Add("apikey", apiKey);
        request.Parameters.Add("apipassword", apiPassword);
        request.Parameters.Add("clientrequestid", clientRequestId);
        
        return request;
    }
}