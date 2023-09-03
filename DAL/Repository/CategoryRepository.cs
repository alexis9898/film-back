using DAL.Data;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly FilmAppContext _context;

        public CategoryRepository(FilmAppContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }

        public async Task<Category> GetOneCategory(int categoryId)
        {
            var category =await _context.Categories.FindAsync(categoryId);
            return category;
        }


        public async Task<Category> AddCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        // PUT/Patch
        public async Task UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return;
        }

        //delete
        public async Task DeleteCategoryAsync(Category category)
        {
            _context.Remove(category);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task<List<Category>> GetCategoriesByFilmId(int filmId)
        {

            //return await _context.Categories
            //    .Where(c => c.FilmCategories
            //    .Any(fc => fc.FilmId == filmId))
            //    .ToListAsync();
            return null;

        }
    }
}
