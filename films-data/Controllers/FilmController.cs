using BLL.Interface;
using BLL.Model;
using DAL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace films_data.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly IFilmService _filmService ;
        private readonly HttpClient _http;

        public FilmController(IFilmService filmService, HttpClient http)
        {
            _filmService = filmService;
            _http = http;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetImages()
        {
            try
            {

                var films= await _filmService.GetFilmsAsync();
                return Ok(films);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("smart")]
        public async Task<IActionResult> smart()
        {
            try
            {
                var list = new List<Dictionary<string, object>>();
                var list1 =new Dictionary<string,object>();



                for (int i = 173; i <= 429; i++)
                {
                    string url = "https://www.playsmart.co.il/wp-admin/admin-ajax.php?action=get_rank_tables&tourId="+i;
                    HttpResponseMessage response = await _http.GetAsync(url);
                    if (response.IsSuccessStatusCode == false)
                    {
                        // Request was not successful
                        //return BadRequest(response);
                        continue;
                    }

                    string responseBody = await response.Content.ReadAsStringAsync();

                    JObject j = JObject.Parse(responseBody);
                    if ((string)j["ResponseMessage"] != "Succes")
                        continue;

                    var day = (j["tournamentInfoList"][0]["dailyRankTable"]);
                    var id = 1195449;
                    foreach (var user in day)
                    {
                        if ((int)user["customerNumber"] != id)
                            continue;
                        if (user["winSum"] == null)
                            continue;
                        var win = user["winSum"];
                        //list.Add(new Dictionary<string, object> { {""+i, win } });
                        list1.Add("i" + i, win);
                    }
                }

                int sum=0;
                foreach (var win in list1.Values)
                {
                    if (win != null)
                    {
                        string price = win.ToString();
                        if (price == null || price == "")
                            continue;
                        sum += int.Parse(price);
                    }

                }
                //foreach (var day in list)
                //{
                //    foreach (var win in day.Values)
                //        if (win != null)
                //        {
                //            string price = win.ToString();
                //            if (price == null || price=="")
                //                continue;
                //            sum+=int.Parse(price);
                //        }
                //}
                return Ok(new{ list1 ,sum });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("by-catrgoryId/{id}")]
        public async Task<IActionResult> GetFimsByCategoryId([FromRoute] int id)
        {
            try
            {
                var films = await _filmService.GetFilmsByCregoryIdAsync(id);
                if (films == null)
                {
                    return NotFound();
                }
                return Ok(films);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("by-name/{name}")]
        public async Task<IActionResult> GetFimsByName([FromRoute] string name)
        {
            try
            {
                var films = await _filmService.GetFilmByNameAsync(name);
                if (films == null)
                {
                    return NotFound();
                }
                return Ok(films);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneImage([FromRoute] int id)
        {
            try
            {
                var film = await _filmService.GetFilmAsync(id);
                if (film == null)
                    return NotFound();
                return Ok(film);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize(Roles ="Admin")]
        [HttpPost("")]
        public async Task<IActionResult> addNewImage([FromBody] FilmModel filmModel)
        {
            try
            {
                var newFilm = await _filmService.AddFilmAsync(filmModel);
                return Ok(newFilm);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateImage([FromRoute] int id, [FromBody] FilmModel filmModel)
        {
            try
            {
                var updateFilm= await _filmService.UpdateFilmAsync(id, filmModel);
                if (updateFilm == null)
                    return NotFound();
                return Ok(updateFilm);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> updateFilmPatch([FromRoute] int id, [FromBody] JsonPatchDocument imagePatch)
        {
            try
            {
                var updateFilm = await _filmService.UpdatePatchFilmAsync(id, imagePatch);
                if (updateFilm == null)
                    return NotFound();
                return Ok(updateFilm);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // DELETE 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var resault = await _filmService.DeleteFilmAsync(id);
                if (resault)
                    return NoContent();
                return BadRequest();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
