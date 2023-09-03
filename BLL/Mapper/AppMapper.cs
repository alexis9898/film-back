using AutoMapper;
using BLL.Model;
using DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapper
{
    public class AppMapper : Profile
    {
        public AppMapper()
        {
            CreateMap<Film, FilmModel>().ForMember(e => e.ImagesModel, opt => opt.MapFrom(src => src.Images))
            .ForMember(x=>x.CategoriesModel,opt=>opt.MapFrom(src=>src.Categories)).ReverseMap();
            CreateMap<Image, ImageModel>().ReverseMap();  
            CreateMap<Category,CategoryModel>().ReverseMap();
        }
    }
}
