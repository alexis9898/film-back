using BLL.Model;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IFilmService
    {
        Task<List<FilmModel>> GetFilmsAsync();
        Task<List<FilmModel>> GetFilmByNameAsync(string value);
        Task<FilmModel> GetFilmAsync(int filmId);
        Task<FilmModel> AddFilmAsync(FilmModel film);
        Task<FilmModel> UpdateFilmAsync(int filmId, FilmModel filmModel);
        Task<FilmModel> UpdatePatchFilmAsync(int filmId, JsonPatchDocument filmPatch);
        Task<bool> DeleteFilmAsync(int filmId);

        Task<List<FilmModel>> GetFilmsByCregoryIdAsync(int id);

    }
}
