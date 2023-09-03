using DAL.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IImageRepository
    {
        Task<List<Image>> GetImages();
        Task<Image> GetOneImage(int imageId);
        Task<Image> addImage(Image image);
        Task UpdateImageAsync(Image image);
        //Task<Image> UpdateImagePatchAsync();
        Task deleteImageAsync(Image image);



    }
}
