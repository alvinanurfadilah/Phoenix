namespace PhoenixWeb.BackendGateway;

public class InventoryBackendGatewayService
{
    public async Task<string> Insert(InventoryFormDTO dto, string token)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5041");
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await client.PostAsJsonAsync("/api/v1/inventory", dto);
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

    public async Task<string> Get(string name, string token)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5041");
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await client.GetAsync($"/api/v1/inventory/{name}");
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

    public async Task<string> Update(InventoryFormDTO dto, string token)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5041");
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await client.PutAsJsonAsync("/api/v1/inventory", dto);
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
