using BLL.Model;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using System.Dynamic;
using DAL.Interfaces;
using DAL.Data;

namespace BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoryModel>> GetCategories()
        {
            var categories = _mapper.Map<List<CategoryModel>>(await _categoryRepository.GetCategories());
            return categories;
        }

        public async Task<List<CategoryModel>> GetCategoriesByFilmId(int filmId)
        {
            var categories = _mapper.Map<List<CategoryModel>>(await _categoryRepository.GetCategoriesByFilmId(filmId));
            return categories;
        }

        public async Task<CategoryModel> GetOneCategory(int categoryId)
        {
            var category = await _categoryRepository.GetOneCategory(categoryId);
            return _mapper.Map<CategoryModel>(category);
        }

        public async Task<CategoryModel> addCategory(CategoryModel categoryModel)
        {
            var newCategory = await _categoryRepository.AddCategory(_mapper.Map<Category>(categoryModel));
            categoryModel.Id = newCategory.Id;
            return categoryModel;
        }
        public async Task<CategoryModel> UpdateCategoryAsync(int categoryId, CategoryModel categoryModel)
        {
            var category = await _categoryRepository.GetOneCategory(categoryId);
            if (category == null)
                return null;
            category.Name = categoryModel.Name;
            category.Id = categoryId;
            await _categoryRepository.UpdateCategoryAsync(category);
            return _mapper.Map<CategoryModel>(category);
        }

        public async Task<CategoryModel> UpdateCategoryPatchAsync(int categoryId, JsonPatchDocument categoryPatch)
        {
            var category = await _categoryRepository.GetOneCategory(categoryId);
            if (category == null)
                return null;
            categoryPatch.ApplyTo(category);
            await _categoryRepository.UpdateCategoryAsync(category);

            return _mapper.Map<CategoryModel>(category);
        }
        public async Task<bool> deleteCategoryAsync(int categoryId)
        {
            var category = await _categoryRepository.GetOneCategory(categoryId);
            if (category == null)
                return false;
            await _categoryRepository.DeleteCategoryAsync(category);
            return true;
        }

       
    }
}
