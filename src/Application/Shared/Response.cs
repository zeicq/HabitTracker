namespace Application.Shared;

public class Response<T>
{
    public T Data { get; }
    public bool Success { get; }
    public List<string> Errors { get; }

    public Response(T data, bool success = true, List<string> errors = null)
    {
        Data = data;
        Success = success;
        Errors = errors ?? new List<string>();
    }
}