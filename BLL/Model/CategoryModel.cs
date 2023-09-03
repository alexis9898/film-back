using BLL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.Model
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        

        public List<FilmModel> FilmsModel{ get; set; }
      
    }
}
