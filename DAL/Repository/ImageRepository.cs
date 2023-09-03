using DAL.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly FilmAppContext _context;

        public ImageRepository(FilmAppContext context)
        {
            _context = context;
        }

        public async Task<List<Image>> GetImages()
        {
            var images = await _context.Images.ToListAsync();
            return images;
        }

        public async Task<Image> GetOneImage(int imageId)
        {
            var image = await _context.Images.FindAsync(imageId);
            return image;
        }

        //add
        public async Task<Image> addImage(Image image)
        {
            _context.Images.Add(image);
            await _context.SaveChangesAsync();
            return image;
        }

        //PUT
        public async Task UpdateImageAsync(Image image)
        {

            _context.Images.Update(image); //????
            await _context.SaveChangesAsync();
            return;
        }

        ////patch
        //public async Task<ImageModel> UpdateImagePatchAsync(int imageId, JsonPatchDocument imagePatch)
        //{
        //    var image = await _context.Images.FindAsync(imageId);
        //    if (image == null)
        //        return null;

        //    imagePatch.ApplyTo(image);
        //    await _context.SaveChangesAsync();

        //    return _mapper.Map<ImageModel>(image);
        //}

        //delete
        public async Task deleteImageAsync(Image image)
        {
            _context.Remove(image);
            await _context.SaveChangesAsync();
        }
    }
}
