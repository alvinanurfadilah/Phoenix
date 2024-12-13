namespace PhoenixWeb.BackendGateway;

public class AccountBackendGatewayService
{
    private readonly IConfiguration _config;

    public AccountBackendGatewayService(IConfiguration config)
    {
        _config = config;
    }

    public async Task<string> ChangePassword(AccountChangePasswordDTO dto, string token)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri(_config.GetSection("AppSettings:API").Value);
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await client.PutAsJsonAsync("/api/v1/account", dto);
        var content = string.Empty;

        if (response.IsSuccessStatusCode)
        {
            content = await response.Content.ReadAsStringAsync();
        } else
        {
            content = response.StatusCode.ToString();
        }

        return content;
    }
}
