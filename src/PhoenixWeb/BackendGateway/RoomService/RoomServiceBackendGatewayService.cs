namespace PhoenixWeb.BackendGateway;

public class RoomServiceBackendGatewayService
{
    public async Task<string> Insert(RoomServiceFormDTO dto, string token)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5041");
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await client.PostAsJsonAsync("/api/v1/roomservice", dto);
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

    public async Task<string> Get(string employeeNumber, string token)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5041");
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await client.GetAsync($"/api/v1/roomservice/{employeeNumber}");
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

    public async Task<string> Update(RoomServiceFormDTO dto, string token)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5041");
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await client.PutAsJsonAsync("/api/v1/roomservice", dto);
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
