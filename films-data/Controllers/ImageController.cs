using BLL.Model;
using BLL.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AppStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService; 

        public ImageController(IImageService imageRepository )
        {
            _imageService = imageRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetImages()
        {
            try
            {
                var images = await _imageService.GetImage();
                return Ok(images);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneImage([FromRoute] int id)
        {
            try
            {
                var image= await _imageService.GetOneImage(id);
                if (image == null)
                    return NotFound();
                return Ok(image);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST 
        [HttpPost("")]
        public async Task<IActionResult> addNewImage([FromBody] ImageModel imageModel)
        {
            try
            {
                var newImage = await _imageService.addImage(imageModel);
                return Ok(newImage);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // PUT 
        [HttpPut("{id}")]
        public async Task<IActionResult> updateImage([FromRoute] int id, [FromBody] ImageModel imageModel)
        {
            try
            {
                var updateImage = await _imageService.UpdateImageAsync(id, imageModel);
                if (updateImage == null)
                    return NotFound();
                return Ok(updateImage);
            }
            catch (Exception)
            {

                throw;
            }
        }

        //patch
        [HttpPatch("{id}")]
        public async Task<IActionResult> updateImagePatch([FromRoute] int id, [FromBody] JsonPatchDocument imagePatch)
        {
            try
            {
                var updateImage = await _imageService.UpdateImagePatchAsync(id, imagePatch);
                if (updateImage == null)
                    return NotFound();
                return Ok(updateImage);
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
                var resault= await _imageService.deleteImageAsync(id);
                if (resault)
                    return NoContent();
                return BadRequest();    
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpPost("uplaod-image-file")]
        public async Task<IActionResult> singleFile ()
        {
            try
            {
                //var dir=_env.ContentRootPath;

                //using (var filestream = new FileStream(Path.Combine(dir, "file.png"), FileMode.Create, FileAccess.Write))
                //{
                //    file.CopyTo(filestream);
                //}

                //return Ok();


                var file = Request.Form.Files[0];
                var angularFolderName = Path.Combine("films", "src","assets","films-images");
                //var dirProject = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var dirProject = Directory.GetCurrentDirectory();
                //string lastFolderName = Path.GetFileName(Path.GetDirectoryName(pathToSave));
                string directoryName = Path.GetDirectoryName(dirProject);  //dir folder of this project
                directoryName = Path.GetDirectoryName(directoryName);
                var myUniqueFileName = $@"{DateTime.Now.Ticks}";
                //var myUniqueFileName = string.Format(@"{0}.txt", DateTime.Now.Ticks);


                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string extention = fileName.Split('.')[1]; //type of file: "jpg, ..."
                    var dbPath = myUniqueFileName +"."+ extention;
                    var fullPath = Path.Combine(directoryName, angularFolderName, dbPath);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(dbPath);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (System.Exception)
            {

                return BadRequest();
            }

        }

        [HttpGet("delete-image-file/{path}")]
        public async Task<IActionResult> DeleteImageFile([FromRoute] string path)
        {
            try
            {
                var dirProject = Directory.GetCurrentDirectory();
                string directoryName = Path.GetDirectoryName(dirProject);  //dir folder of this project
                var angularFolderName = Path.Combine("AppAn", "src", "assets", "product-images");
                var fullPath = Path.Combine(directoryName, angularFolderName, path);
                System.IO.File.Delete(fullPath);
                return NoContent();

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}
