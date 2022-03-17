using Newtonsoft.Json;

namespace NetcupApi.Common;

public class DnsRecord
{
    [JsonProperty(PropertyName = "id")] 
    public int Id { get; set; }
    [JsonProperty(PropertyName = "hostname")]
    public string Hostname { get; set; }
    [JsonProperty(PropertyName = "type")]
    public string Type { get; set; }
    [JsonProperty(PropertyName = "priority")]
    public string Priority { get; set; }
    [JsonProperty(PropertyName = "destination")]
    public string Destination { get; set; }
    [JsonProperty(PropertyName = "deleterecord")]
    public bool Deleterecord { get; set; }
    [JsonProperty(PropertyName = "state")]
    public string State { get; set; }
}