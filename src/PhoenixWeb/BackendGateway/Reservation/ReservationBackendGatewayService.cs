namespace PhoenixWeb.BackendGateway;

public class ReservationBackendGatewayService
{
    public async Task<string> Get(string token)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5041");
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await client.GetAsync($"/api/v1/reservation");
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

    public async Task<string> TotalIncome(int month, int year, string token)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5041");
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await client.GetAsync($"/api/v1/reservation/totalincome/{month}/{year}");
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
