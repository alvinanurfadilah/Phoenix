namespace PhoenixWeb.BackendGateway;

public class AdminBackendGatewayService
{
    private readonly IConfiguration _config;

    public AdminBackendGatewayService(IConfiguration config)
    {
        _config = config;
    }
    public async Task<string> Insert(AdminFormDTO dto, string token)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri(_config.GetSection("AppSettings:API").Value);
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await client.PostAsJsonAsync("/api/v1/admin", dto);
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

    public async Task<string> Get(string username, string token)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5041");
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await client.GetAsync($"/api/v1/admin/{username}");
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

    public async Task<string> Update(AdminFormDTO dto, string token)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5041");
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await client.PutAsJsonAsync("/api/v1/admin", dto);
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
