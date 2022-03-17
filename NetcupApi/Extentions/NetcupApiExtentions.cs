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
}