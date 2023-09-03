using BLL.Model;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryModel>> GetCategories();
        Task<CategoryModel> GetOneCategory(int categoryId);
        Task<CategoryModel> addCategory(CategoryModel categoryModel);
        Task<CategoryModel> UpdateCategoryAsync(int categoryId, CategoryModel categoryModel);
        Task<CategoryModel> UpdateCategoryPatchAsync(int categoryId, JsonPatchDocument categoryPatch);
        Task<bool> deleteCategoryAsync(int categoryId);
        Task<List<CategoryModel>> GetCategoriesByFilmId(int filmId);

    }
}
