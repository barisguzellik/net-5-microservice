using System;
using System.Collections.Generic;
using CategoryService.DTOs.Requests;
using CategoryService.DTOs.Responses;
using CategoryService.Models;

namespace CategoryService.Repositories
{
    public interface ICategoryRepository
    {
        public List<Categories> GetAll(GetAllCategoriesRequest request);
        public Categories FindById(FindByIdCategoryRequest request);
        public Categories Create(CreateCategoryRequest request);
        public bool Update(UpdateCategoryRequest request);
        public bool Delete(DeleteCategoryRequest request);
    }
}
