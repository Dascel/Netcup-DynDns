using Microsoft.Extensions.Configuration;
using Netcup_DynDns.PublicIp;
using NetcupApi.Common;
using NetcupApi.Netcup;

var configurationRoot = new ConfigurationBuilder()
    .AddJsonFile("config.json", true)
    .Build();

var apiKey = configurationRoot["apiKey"];
var apiPassword = configurationRoot["apiPassword"];
var customerNumber = Convert.ToInt32(configurationRoot["customerNumber"]);
var domainName = configurationRoot["domainName"];
var dynDnsSubDomainName = configurationRoot["dynDnsSubDomainName"];

Console.WriteLine($"Starting ip check");
var httpClient = new HttpClient();
var api = new Api(httpClient);

var loginResult = await api.Login(customerNumber, apiKey, apiPassword);
if (loginResult.ResponseData == null)
{
    Console.WriteLine("Could not get session, aborting.");
    return;
}
var recordsResult = await api.InfoDnsRecords(domainName, customerNumber, apiKey, loginResult.ResponseData.ApiSessionId);
var dynDnsRecord = recordsResult.ResponseData.DnsRecords.FirstOrDefault(dr => dr.Hostname.Equals(dynDnsSubDomainName));

var publicIpCheck = new PublicIp(httpClient);
var currentPublicIp = await publicIpCheck.GetPublicIp();

if (!currentPublicIp.Equals(dynDnsRecord.Destination))
{
    dynDnsRecord.Destination = currentPublicIp;
    
    await api.UpdateDnsRecords(domainName, customerNumber, apiKey, loginResult.ResponseData.ApiSessionId,
        new DnsRecordSet()
        {
            DnsRecords = new List<DnsRecord>()
            {
                dynDnsRecord
            }
        });
    Console.WriteLine($"DynDns record updated to {currentPublicIp}");
}
else
{
    Console.WriteLine($"DynDns record not updated is already {currentPublicIp}");
}

await api.Logout(customerNumber, apiKey, loginResult.ResponseData.ApiSessionId);
Console.WriteLine("Check done.");