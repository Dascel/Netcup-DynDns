using Newtonsoft.Json;

namespace NetcupApi.Common;

public class ResponseMessage<T>
{
    [JsonProperty(PropertyName = "serverrequestid")]
    public string ServerRequestId { get; set; }
    [JsonProperty(PropertyName = "clientrequestid")]
    public string ClientRequestId { get; set; }
    [JsonProperty(PropertyName = "action")]
    public string Action { get; set; }
    [JsonProperty(PropertyName = "status")]
    public string Status { get; set; }
    [JsonProperty(PropertyName = "statuscode")]
    public int StatusCode { get; set; }
    [JsonProperty(PropertyName = "shortmessage")]
    public string ShortMessage { get; set; }
    [JsonProperty(PropertyName = "longmessage")]
    public string LongMessage { get; set; }
    [JsonProperty(PropertyName = "responsedata")]
    public T ResponseData { get; set; }
}