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
var dynDnsSubDomains = configurationRoot.GetSection("dynDnsSubDomains").Get<string[]>();

if (string.IsNullOrEmpty(apiKey) ||
    string.IsNullOrEmpty(apiPassword) ||
    string.IsNullOrEmpty(domainName) ||
    dynDnsSubDomains.Length == 0)
{
    Console.WriteLine("Not all needed config values are set, aborting.");
    return;
}

Console.WriteLine("Starting ip check");
var httpClient = new HttpClient();
var api = new Api(httpClient);

var loginResult = await api.LoginAsync(customerNumber, apiKey, apiPassword);
if (loginResult.ResponseData == null)
{
    Console.WriteLine("Could not get session, aborting.");
    return;
}

var recordsResult =
    await api.InfoDnsRecordsAsync(domainName, customerNumber, apiKey, loginResult.ResponseData.ApiSessionId);

var publicIpCheck = new PublicIp(httpClient);
var currentPublicIp = await publicIpCheck.GetPublicIp();

var editList = new List<DnsRecord>();
foreach (var subDomain in dynDnsSubDomains)
{
    var dynDnsRecord = recordsResult.ResponseData.DnsRecords.FirstOrDefault(dr => dr.Hostname.Equals(subDomain));
    if (!currentPublicIp.Equals(dynDnsRecord.Destination))
    {
        dynDnsRecord.Destination = currentPublicIp;
        editList.Add(dynDnsRecord);
        Console.WriteLine($"DynDns record ({dynDnsRecord.Hostname}) updated to {currentPublicIp}");
    }
    else
    {
        Console.WriteLine($"DynDns record ({dynDnsRecord.Hostname}) not updated is already {currentPublicIp}");
    }
}

if(editList.Any())
    await api.UpdateDnsRecordsAsync(domainName, customerNumber, apiKey, loginResult.ResponseData.ApiSessionId,
        new DnsRecordSet() {DnsRecords = editList});

await api.LogoutAsync(customerNumber, apiKey, loginResult.ResponseData.ApiSessionId);
Console.WriteLine("Check done.");