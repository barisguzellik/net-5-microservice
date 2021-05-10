using System;
using Newtonsoft.Json;

namespace CategoryService.DTOs.Requests
{
    public class CreateCategoryRequest
    {
        public string name { get; set; }
        public string slug { get; set; }
        public int is_active { get; set; }
        public int sort { get; set; }
    }
}
