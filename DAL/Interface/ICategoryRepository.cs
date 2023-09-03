using DAL.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();
        Task<Category> GetOneCategory(int categoryId);
        Task<Category> AddCategory(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(Category category);
        Task<List<Category>> GetCategoriesByFilmId(int filmId);


    }
}
