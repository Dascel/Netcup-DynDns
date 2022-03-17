using Newtonsoft.Json;

namespace NetcupApi.Common;

public class DnsRecordSet
{
    [JsonProperty(PropertyName = "dnsrecords")]
    public List<DnsRecord> DnsRecords { get; set; }
}