
using System.Collections.Generic;

namespace DAL.Data
{
    public class Category
    {
        public int Id { get; set; }
        public string Name{ get; set; }

        public List<FilmCategory> FilmCategories { get; set; }
        public List<Film> Films { get; set; }
    }
}
