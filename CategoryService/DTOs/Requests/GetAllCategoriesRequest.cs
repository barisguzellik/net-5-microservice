using System;
namespace CategoryService.DTOs.Requests
{
    public class GetAllCategoriesRequest
    {
        public int page { get; set; }
        public int offset { get; set; }
    }
}
