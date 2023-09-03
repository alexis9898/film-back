using AutoMapper;
using BLL.Interfaces;
using BLL.Model;
using DAL.Data;
using DAL.Repository;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ImageService:IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;
        public ImageService(IMapper mapper, IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public async Task<List<ImageModel>> GetImage()
        {
            var images = await _imageRepository.GetImages();
            return _mapper.Map<List<ImageModel>>(images);
        }
       
        public async Task<ImageModel> GetOneImage(int imageId)
        {
            var image = await _imageRepository.GetOneImage(imageId);
            if (image == null)
                return null;
            return _mapper.Map<ImageModel>(image);
        }

        //add
        public async Task<ImageModel> addImage(ImageModel image)
        {
            //var newCategory = new Category()
            //{
            //    Name = category.Name,
            //    ParentId = category.ParentId,
            //};

            var newImage = _mapper.Map<Image>(image);
            newImage= await _imageRepository.addImage(newImage);
            image = _mapper.Map<ImageModel>(newImage);
            return image;
        }

        //PUT
        public async Task<ImageModel> UpdateImageAsync(int imageId, ImageModel imageModel)
        {
            var image = await _imageRepository.GetOneImage(imageId);
            if (image == null)
                return null;

            image.Path = imageModel.Path;
            image.FilmId = imageModel.FilmId;

            await _imageRepository.UpdateImageAsync(image);
            imageModel.Id = imageId;
            return imageModel;
        }

        //patch
        public async Task<ImageModel> UpdateImagePatchAsync(int imageId, JsonPatchDocument imagePatch)
        {
            var image = await _imageRepository.GetOneImage(imageId);
            if (image == null)
                return null;
            imagePatch.ApplyTo(image);
            await _imageRepository.UpdateImageAsync(image);
            return _mapper.Map<ImageModel>(image);
        }

        //delete
        public async Task<bool> deleteImageAsync(int imageId)
        {
            var image = await _imageRepository.GetOneImage(imageId);
            if (image == null)
                return false;
            await _imageRepository.deleteImageAsync(image);
            return true;
        }

        //public async Task<object> singleFile(object file)
        //{
        //    //var file = Request.Form.Files[0];
        //    var angularFolderName = Path.Combine("AppAn", "src", "assets", "product-images");
        //    //var dirProject = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        //    var dirProject = Directory.GetCurrentDirectory();
        //    //string lastFolderName = Path.GetFileName(Path.GetDirectoryName(pathToSave));
        //    string directoryName = Path.GetDirectoryName(dirProject);  //dir folder of this project

        //    var myUniqueFileName = $@"{DateTime.Now.Ticks}";
        //    //var myUniqueFileName = string.Format(@"{0}.txt", DateTime.Now.Ticks);


        //    if (file.Length > 0)
        //    {
        //        string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        //        string extention = fileName.Split('.')[1]; //type of file: "jpg, ..."
        //        var dbPath = myUniqueFileName + "." + extention;
        //        var fullPath = Path.Combine(directoryName, angularFolderName, dbPath);
        //        using (var stream = new FileStream(fullPath, FileMode.Create))
        //        {
        //            file.CopyTo(stream);
        //        }
        //        return dbPath;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}


    }
}
