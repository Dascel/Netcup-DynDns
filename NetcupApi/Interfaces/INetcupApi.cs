using NetcupApi.Common;

namespace NetcupApi.Interfaces;

public interface INetcupApi
{
    public Task<ResponseMessage<SessionObject>?> LoginAsync(int customerNumber, string apiKey, string apiPassword,
        string clientRequestId = "");
    
    public Task<ResponseMessage<string>?> LogoutAsync(int customerNumber, string apiKey, string apiSessionId, string clientRequestId = "");

    public Task<ResponseMessage<DnsRecordSet>?> InfoDnsRecordsAsync(string domainName, int customerNumber, string apiKey, string apiSessionId, string clientRequestId = "");
    
    public Task<ResponseMessage<DnsRecordSet>?> UpdateDnsRecordsAsync(string domainName, int customerNumber, string apiKey, string apiSessionId, DnsRecordSet dnsRecordSet, string clientRequestId = "");
}

