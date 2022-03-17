using Newtonsoft.Json;

namespace NetcupApi.Common;

public class SessionObject
{
    [JsonProperty(PropertyName = "apisessionid")]
    public string ApiSessionId  { get; set; }
}