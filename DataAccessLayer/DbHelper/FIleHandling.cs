using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.DbHelper
{
    public class FIleHandling
    {
        //private readonly IWebHostEnvironment _webHostEnvironment;


        private static string GetFilePath(IFormFile request)
        {
            var fileName = request.FileName.Substring(0, request.FileName.LastIndexOf('.'));
            var ext = request.FileName.Substring(request.FileName.LastIndexOf('.'), request.FileName.Length - request.FileName.LastIndexOf('.'));
            fileName += Guid.NewGuid().ToString();
            var filePath = fileName + ext;
            return filePath;
        }// Generate Uniq FileName for CloudinaryFile and return 

        public static async Task<string >FileUpload(IConfiguration _config, IFormFile file)
        {
            try
            {

                var cloudinary = new Cloudinary(new Account(_config.GetSection("Cloudanry:Name").Value, _config.GetSection("Cloudanry:APIKey").Value, _config.GetSection("Cloudanry:API Secret").Value));

                // Upload

                var stream = file.OpenReadStream();

                var uploadParams = new ImageUploadParams()
                {
                    //Getting Details

                    File = new FileDescription(GetFilePath(file), stream),
                    PublicId = file.FileName,
                    Folder = "Trail",
                };
                var uploadResult = await cloudinary.UploadAsync(uploadParams);
                if(uploadResult != null)
                {
                    return uploadResult.Url.ToString();
                }
                return "File Not Uploaded";
            }
            catch (Exception)
            {

                throw;
            }
        }// Uploading File To Cloudinary and returning URL 
    }
}
