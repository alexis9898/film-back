using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Model
{
    public class FilmModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int Like { get; set; }
        public double Price { get; set; }
        public double Imdb { get; set; }
        public string Discription { get; set; }
        public List<CategoryModel> CategoriesModel { get; set; }
        public List<ImageModel> ImagesModel { get; set; }

    }
}
