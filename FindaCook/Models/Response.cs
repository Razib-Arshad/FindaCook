using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using FindaCook.Models;


namespace FindaCook.Maui.Models
{
    public class Response
    {
        [JsonPropertyName("user")]
        public User User { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
    public class FavResponseContainer
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<FavouriteCookDetails> Data { get; set; }
    }
    public class orderResponseContainer
    {

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<OrderRequest> Data { get; set; }  // Changed from List<SimpleOrderDTO> to List<OrderRequest>
    }

    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
    }

    public class CountApiResponse
    {
        public int Count { get; set; }
        public string Message { get; set; }
    }
}
