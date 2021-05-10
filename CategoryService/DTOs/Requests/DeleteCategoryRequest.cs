using System;
using Newtonsoft.Json;

namespace CategoryService.DTOs.Requests
{
    public class DeleteCategoryRequest
    {
        public int category_id { get; set; }
    }
}
