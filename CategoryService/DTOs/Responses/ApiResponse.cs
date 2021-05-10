using System;
namespace CategoryService.DTOs.Responses
{
    public class ApiResponse<TRequest, TResponse>
    {
        public bool status { get; set; }
        public string error_code { get; set; }
        public string error_message { get; set; }
        public TRequest request { get; set; }
        public TResponse response { get; set; }
    }
}
