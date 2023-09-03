using DAL.Data;
using DAL.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class FilmRepository: IFilmRepository
    {
        private readonly FilmAppContext _context;

        public FilmRepository(FilmAppContext context)
        {
            _context = context;
        }

        public async Task<List<Film>> GetFilmsAsync()
        {
            var films = await _context.Films
                .Include(f=>f.Images)
                .Include(x=>x.Categories)
                .ToListAsync();
            return films;
        }

        public async Task<Film> GetFilmAsync(int filmId)
        {
            var films = await _context.Films.Where(x=>x.Id==filmId).Include(f=>f.Images).Include(f=>f.Categories).FirstOrDefaultAsync();
            return films;
        }

        public async Task<List<Film>> GetFilmByNameAsync(string filmName)
        {
            var films = await _context.Films.Where(x => x.Name.ToLower().Contains(filmName.ToLower())).Include(f => f.Images).Include(f=>f.Categories).Take(3).ToListAsync();
            return films;
        }

        //add
        public async Task<Film> AddFilmAsync(Film film)
        {
            _context.Films.Add(film);


            await _context.SaveChangesAsync();
            return film;
        }

        // PUT/PATCH 
        public async Task UpdateFilmAsync(Film film)
        {

            //_context.Images.Update(film); ????
            _context.Films.Update(film); 
            await _context.SaveChangesAsync();
            return;
        }

        //delete
        public async Task DeleteFilmAsync(Film film)
        {
            _context.Remove(film);
            await _context.SaveChangesAsync();
        }


        public async Task<List<Film>> GetFilmsByCategoryId(int categoryId)
        {
            return await _context.Films
                .Where(f => f.FilmCategories.Any(fc => fc.CategoryId == categoryId)).Include(x => x.Images).Include(x=>x.Categories)
                .ToListAsync();
            return null;
        }

        //post FilmCategory
        public async Task<FilmCategory> ConectFilmToCategory(FilmCategory filmCategory)
        {
            _context.Add(filmCategory);
             await _context.SaveChangesAsync();
            return filmCategory;
        }
        //updat FilmCategory


    }
}
