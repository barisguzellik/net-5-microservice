using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CategoryService.DTOs.Requests;
using CategoryService.DTOs.Responses;
using CategoryService.Models;
using CategoryService.Repositories;
using CategoryService.Validations;
using Microsoft.AspNetCore.Mvc;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CategoryService.Controllers
{
    [Route("api/v1")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _repository;
        private readonly CreateCategoryValidations _createValidations;
        private readonly GetAllCategoriesValidations _gelAllValidations;
        private readonly FindByIdCategoryValidations _findByIdCategoryValidations;
        private readonly DeleteCategoryValidations _deleteCategoryValidations;
        private readonly UpdateCategoryValidations _updateCategoryValidations;

        private readonly IQueueRepository _queueRepository;

        public CategoryController
        (
            ICategoryRepository repository,
            CreateCategoryValidations createValidations,
            GetAllCategoriesValidations getAllValidations,
            FindByIdCategoryValidations findByIdCategoryValidations,
            DeleteCategoryValidations deleteCAtegoryValidations,
            UpdateCategoryValidations updateCategoryValidations,
            IQueueRepository queueRepository
        )
        {
            _repository = repository;
            _createValidations = createValidations;
            _gelAllValidations = getAllValidations;
            _findByIdCategoryValidations = findByIdCategoryValidations;
            _deleteCategoryValidations = deleteCAtegoryValidations;
            _updateCategoryValidations = updateCategoryValidations;
            _queueRepository = queueRepository;
        }

        // GET ALL DATA
        [Route("category/all")]
        [HttpGet]
        public ApiResponse<GetAllCategoriesRequest, List<Categories>> Get(GetAllCategoriesRequest request)
        {
            var validate = _gelAllValidations.Validate(request);
            if (!validate.IsValid)
            {
                Log.Error(validate.ToString(""));

                return new ApiResponse<GetAllCategoriesRequest, List<Categories>>()
                {
                    status = false,
                    error_code = "validations_error",
                    error_message = validate.ToString("~"),
                    request = request
                };
            }

            var response = _repository.GetAll(request);

            Log.Information("All Categories returned...");

            return new ApiResponse<GetAllCategoriesRequest, List<Categories>>()
            {
                status = true,
                request = request,
                response = response
            };
        }

        // FIND BY ID
        [Route("category/{category_id}")]
        [HttpGet]
        public ApiResponse<FindByIdCategoryRequest, Categories> Get(FindByIdCategoryRequest request)
        {
            var validate = _findByIdCategoryValidations.Validate(request);
            if (!validate.IsValid)
            {
                Log.Error(validate.ToString(""));

                return new ApiResponse<FindByIdCategoryRequest, Categories>()
                {
                    status = false,
                    error_code = "validations_error",
                    error_message = validate.ToString("~"),
                    request = request
                };
            }

            var response = _repository.FindById(request);

            Log.Information("Category returned...");

            return new ApiResponse<FindByIdCategoryRequest, Categories>()
            {
                status = true,
                request = request,
                response = response
            };

        }

        //CREATE
        [Route("category/create")]
        [HttpPost]
        public ApiResponse<CreateCategoryRequest, Categories> Post([FromBody] CreateCategoryRequest request)
        {
            var validate = _createValidations.Validate(request);
            if (!validate.IsValid)
            {
                Log.Error(validate.ToString(""));

                return new ApiResponse<CreateCategoryRequest, Categories>()
                {
                    status = false,
                    error_code = "validations_error",
                    error_message = validate.ToString("~"),
                    request = request
                };
            }

            var response = _repository.Create(request);

            Log.Information("Category added...");

            _queueRepository.Add(request);

            Log.Information("Category queue added...");

            _queueRepository.Read();

            return new ApiResponse<CreateCategoryRequest, Categories>()
            {
                status = true,
                request = request,
                response = response
            };
        }

        // UPDATE
        [Route("category/update")]
        [HttpPut]
        public ApiResponse<UpdateCategoryRequest, bool> Put([FromBody] UpdateCategoryRequest request)
        {
            var validate = _updateCategoryValidations.Validate(request);
            if (!validate.IsValid)
            {
                Log.Error(validate.ToString(""));

                return new ApiResponse<UpdateCategoryRequest, bool>()
                {
                    status = false,
                    error_code = "validations_error",
                    error_message = validate.ToString("~"),
                    request = request
                };
            }

            var response = _repository.Update(request);

            Log.Information("Category updated...");

            return new ApiResponse<UpdateCategoryRequest, bool>()
            {
                status = true,
                request = request,
                response = response
            };
        }


        // DELETE
        [Route("category/delete")]
        [HttpDelete]
        public ApiResponse<DeleteCategoryRequest, bool> Delete([FromBody] DeleteCategoryRequest request)
        {
            var validate = _deleteCategoryValidations.Validate(request);
            if (!validate.IsValid)
            {
                Log.Error(validate.ToString(""));

                return new ApiResponse<DeleteCategoryRequest, bool>()
                {
                    status = false,
                    error_code = "validations_error",
                    error_message = validate.ToString("~"),
                    request = request
                };
            }

            var response = _repository.Delete(request);

            Log.Information("Category deleted...");

            return new ApiResponse<DeleteCategoryRequest, bool>()
            {
                status = true,
                request = request,
                response = response
            };
        }
    }
}
