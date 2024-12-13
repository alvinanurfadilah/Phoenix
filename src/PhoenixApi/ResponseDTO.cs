namespace PhoenixApi;

public class ResponseDTO<T>
{
    public string? Message { get; set; }
    public string? Status { get; set; }
    public T? Data { get; set; }
}
