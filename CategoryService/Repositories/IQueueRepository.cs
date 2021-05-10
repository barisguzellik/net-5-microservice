using System;
using CategoryService.DTOs.Requests;

namespace CategoryService.Repositories
{
    public interface IQueueRepository
    {
        public bool Add(CreateCategoryRequest request);
        public bool Read();
    }
}
