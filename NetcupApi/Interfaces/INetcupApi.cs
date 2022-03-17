using NetcupApi.Common;

namespace NetcupApi.Interfaces;

public interface INetcupApi
{
    public Task<ResponseMessage<SessionObject>?> Login(int customerNumber, string apiKey, string apiPassword,
        string clientRequestId = "");
    
    public Task<ResponseMessage<string>?> Logout(int customerNumber, string apiKey, string apiSessionId, string clientRequestId = "");

    public Task<ResponseMessage<DnsRecordSet>?> InfoDnsRecords(string domainName, int customerNumber, string apiKey, string apiSessionId, string clientRequestId = "");
    
    public Task<ResponseMessage<DnsRecordSet>?> UpdateDnsRecords(string domainName, int customerNumber, string apiKey, string apiSessionId, DnsRecordSet dnsRecordSet, string clientRequestId = "");
}

