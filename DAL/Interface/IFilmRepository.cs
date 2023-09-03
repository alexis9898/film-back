using DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IFilmRepository 
    {
        Task<List<Film>> GetFilmsAsync();
        Task<Film> GetFilmAsync(int filmId);
        Task<List<Film>> GetFilmByNameAsync(string filmName);
        Task<Film> AddFilmAsync(Film film);
        Task UpdateFilmAsync(Film film);
        Task DeleteFilmAsync(Film film);
       
        Task<List<Film>> GetFilmsByCategoryId(int categoryId);
        Task<FilmCategory> ConectFilmToCategory(FilmCategory filmCategory);


    }
}
