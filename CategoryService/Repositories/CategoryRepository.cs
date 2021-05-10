using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CategoryService.DbContexts;
using CategoryService.DTOs.Requests;
using CategoryService.DTOs.Responses;
using CategoryService.Models;
using Mapster;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Serilog;

namespace CategoryService.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoryContext _context;
        private readonly IDistributedCache _distibutedCache;
        public CategoryRepository(CategoryContext context, IDistributedCache distributedCache)
        {
            _context = context;
            _distibutedCache = distributedCache;
        }

        public Categories Create(CreateCategoryRequest request)
        {
            var added = request.Adapt<Categories>();
            _context.Categories.Add(added);
            _context.SaveChanges();
            return added;
        }

        public bool Delete(DeleteCategoryRequest request)
        {
            var delete = _context.Categories.Where(w => w.category_id == request.category_id).FirstOrDefault();

            if (delete != null)
            {
                _context.Remove(delete);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Categories FindById(FindByIdCategoryRequest request)
        {
            int key = request.category_id;
            Categories category;
            string json;

            try
            {
                var categoryFromCache = _distibutedCache.Get(key.ToString());
                if (categoryFromCache != null)
                {
                    json = Encoding.UTF8.GetString(categoryFromCache);
                    Log.Information(request.category_id + " getting from cache...");
                    return JsonConvert.DeserializeObject<Categories>(json);
                }
                else
                {
                    category = _context.Categories.Where(w => w.category_id == request.category_id).FirstOrDefault();
                    json = JsonConvert.SerializeObject(category);
                    categoryFromCache = Encoding.UTF8.GetBytes(json);

                    var options = new DistributedCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromHours(1))
                        .SetAbsoluteExpiration(DateTime.Now.AddHours(2));

                    _distibutedCache.Set(key.ToString(), categoryFromCache, options);

                    Log.Information(request.category_id + " added to cache...");
                }
            }
            catch (Exception ex)
            {
                category = _context.Categories.Where(w => w.category_id == request.category_id).FirstOrDefault();

                Log.Error(ex.Message);
            }
            

            return category;
        }

        public List<Categories> GetAll(GetAllCategoriesRequest request)
        {
            return _context.Categories.Skip((request.page - 1) * request.offset).Take(request.offset).ToList();
        }

        public bool Update(UpdateCategoryRequest request)
        {
            var update = _context.Categories.Where(w => w.category_id == request.category_id).FirstOrDefault();
            if (update == null)
            {
                return false;
            }
            else
            {
                update.name = request.name;
                update.slug = request.slug;
                update.is_active = request.is_active;
                update.sort = request.sort;
                _context.SaveChanges();
                return true;
            }
        }
    }
}
