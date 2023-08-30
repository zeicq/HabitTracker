using System.Text.Json.Serialization;

namespace Application.Shared;

public class Response<T>
{
    [System.Text.Json.Serialization.JsonIgnore]
    public bool Succeeded { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Message { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string> Errors { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T Data { get; set; }

    public Response()
    {
    }

    public Response(T data, string message = null)
    {
        Message = message;
        Data = data;
    }

    public Response(string message)
    {
        Succeeded = false;
        Message = message;
    }
}