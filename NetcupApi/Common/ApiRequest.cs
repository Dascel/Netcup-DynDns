using Newtonsoft.Json;

namespace NetcupApi.Common;

public class ApiRequest
{
    [JsonProperty(PropertyName = "action")] public string Action { get; set; }
    [JsonProperty(PropertyName = "param")] public Dictionary<string, object> Parameters { get; set; }

    public ApiRequest(string action)
    {
        Action = action;
        Parameters = new Dictionary<string, object>();
    }
}