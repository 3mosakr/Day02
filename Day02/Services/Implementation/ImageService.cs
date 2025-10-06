using Day02.Services.Interfaces;
using Microsoft.Extensions.FileProviders;

namespace Day02.Services.Implementation
{
    public class ImageService : IImageService
    {
        private readonly IFileProvider _fileProvider;
        public ImageService(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }
        public async Task<string> AddImageAsync(IFormFile file)
        {
            var SaveImageSrc = "";

            var ImageDirctory = Path.Combine("wwwroot", "Images", "Instructor");
            if (Directory.Exists(ImageDirctory) is not true)
                Directory.CreateDirectory(ImageDirctory);


            if (file.Length > 0)
            {
                // Get Image name
                var FileName = Guid.NewGuid().ToString();
                var Extension = Path.GetExtension(file.FileName);

                var ImageName = FileName + Extension;
                var ImageSrc = $"/Images/Instructor/{ImageName}";
                // for save image in server
                var root = Path.Combine(ImageDirctory, ImageName);
                using (FileStream stream = new(root, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                SaveImageSrc = ImageSrc;
            }

            return SaveImageSrc;
        }

        public void DeleteImageAsync(string src)
        {
            var info = _fileProvider.GetFileInfo(src);
            if (info != null)
            {
                var root = info.PhysicalPath;
                File.Delete(root);
            }
        }

        
    }
}
