using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CategoryService.DTOs.Requests
{
    public class FindByIdCategoryRequest
    {
        public int category_id { get; set; }
    }
}
