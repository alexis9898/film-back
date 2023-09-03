using BLL.Interface;
using DAL.Interface;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Model;
using DAL.Data;
using Microsoft.AspNetCore.JsonPatch;

namespace BLL.Service
{
    public class FilmService: IFilmService
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IMapper _mapper;

        public FilmService(IFilmRepository filmRepository , IMapper mapper)
        {
            _filmRepository = filmRepository;
            _mapper = mapper;
        }

        public async Task<List<FilmModel>> GetFilmsAsync()
        {
            var films = await _filmRepository.GetFilmsAsync();
           // for (int i = 0; i < films.Count; i++)
            //{
              //  var categories=new List<CategoryModel>();
            //}
            var filmsModel= _mapper.Map<List<FilmModel>>(films);
            return filmsModel;
        }

        public async Task<List<FilmModel>> GetFilmByNameAsync(string value)
        {
            var films = await _filmRepository.GetFilmByNameAsync(value);
            var filmsModel = _mapper.Map<List<FilmModel>>(films);
            return filmsModel;
        }

        public async Task<FilmModel> GetFilmAsync(int filmId)
        {
            var film = await _filmRepository.GetFilmAsync(filmId);
            return _mapper.Map<FilmModel>(film);
        }

        //add
        public async Task<FilmModel> AddFilmAsync(FilmModel film)
        {
            var categories=film.CategoriesModel;
            var NewFilm = _mapper.Map<Film>(film);
            NewFilm = await _filmRepository.AddFilmAsync(NewFilm);
            film= _mapper.Map<FilmModel>(NewFilm);
            for (int i = 0; i < categories.Count; i++)
            {
                var categoryId = categories[i].Id;
                var filmCategory = new FilmCategory() {
                    CategoryId = categoryId,
                    FilmId = film.Id
                };
                var res=await _filmRepository.ConectFilmToCategory(filmCategory);
            }
            film.CategoriesModel = categories;
            return film;
        }

        // PUT
        public async Task<FilmModel> UpdateFilmAsync(int filmId, FilmModel filmModel)
        {

            var film = await _filmRepository.GetFilmAsync(filmId);
            if (film == null)
                return null;


            film.Discription = filmModel.Discription;   
            film.Like= filmModel.Like;  
            film.Name= filmModel.Name;
            film.Imdb = filmModel.Imdb;


            await _filmRepository.UpdateFilmAsync(film);
            filmModel.Id= filmId;
            return filmModel;
        }

        //PATCH
        public async Task<FilmModel> UpdatePatchFilmAsync(int filmId, JsonPatchDocument filmPatch)
        {
            var film = await _filmRepository.GetFilmAsync(filmId);
            if (film == null)
                return null;

            filmPatch.ApplyTo(film);
            await _filmRepository.UpdateFilmAsync(film);
            return _mapper.Map<FilmModel>(film);
        }


        //delete
        public async Task<bool> DeleteFilmAsync(int filmId)
        {
            var film = await _filmRepository.GetFilmAsync(filmId);
            if (film == null)
                return false;

            await _filmRepository.DeleteFilmAsync(film);
            return true;
        }
        //get filmd by ID
        public async Task<List<FilmModel>> GetFilmsByCregoryIdAsync(int id)
        {
            var films = await _filmRepository.GetFilmsByCategoryId(id);
            if (films == null)
                return null;

            var filmsModel = _mapper.Map<List<FilmModel>>(films);
            return filmsModel;
        }
    }
}
