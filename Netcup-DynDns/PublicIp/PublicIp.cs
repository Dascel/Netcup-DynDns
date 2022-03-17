namespace Netcup_DynDns.PublicIp;

public class PublicIp
{
    private readonly HttpClient _httpClient;
    private const string IpCheckUrl = "http://checkip.dyndns.org/";

    public PublicIp(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<string> GetPublicIp()
    {
        try
        {
            var returnValue = await _httpClient.GetAsync(IpCheckUrl);
            var content = await returnValue.Content.ReadAsStringAsync();

            return content.Split(":")[1][1..].Split("<")[0];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}