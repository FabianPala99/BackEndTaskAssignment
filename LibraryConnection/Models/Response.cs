using System.Net;
using System.Text.Json.Serialization;

namespace LibraryConnection.Models
{
    public class Response<T>
    {
        [JsonConstructor]
        public Response(bool success, HttpStatusCode status, T? data, string? method = null, string? error = null)
        {
            this.success = success;
            this.status = status;
            response = data;
            this.method = method;
            errors = error;
        }

        public Response(bool success, HttpStatusCode status, string? method = null, string? error = null)
        {
            this.success = success;
            this.status = status;
            this.method = method;
            errors = error;
        }

        public Response(bool success, HttpStatusCode status, string? error = null)
        {
            this.success = success;
            this.status = status;
            errors = error;
        }

        public Response(bool success, HttpStatusCode status)
        {
            this.success = success;
            this.status = status;
        }

        public bool success { get; set; }
        public HttpStatusCode status { get; set; }
        public string? method { get; set; }
        public string? errors { get; set; }
        public T? response { get; set; }
    }
}
