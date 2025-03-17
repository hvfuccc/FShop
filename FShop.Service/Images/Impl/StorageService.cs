using FShop.Utilities.Exceptions;
using Microsoft.AspNetCore.Hosting;

namespace FShop.Service.Images.Impl
{
    public class StorageService(IWebHostEnvironment webHostEnviroment) : IStorageService
    {
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        private readonly string _userContentFolder = Path.Combine(webHostEnviroment.WebRootPath, USER_CONTENT_FOLDER_NAME);

        public async Task DeleteFileAsync(string filePath)
        {
            try
            {
                var fileName = Path.Combine(_userContentFolder, filePath);
                if (File.Exists(fileName))
                {
                    await Task.Run(() => File.Delete(fileName));
                }
            }
            catch (Exception)
            {
                throw new FShopNotImplementedException();
            }
        }

        public string GetFileUrl(string filePath)
        {
            try
            {
                return $"/{USER_CONTENT_FOLDER_NAME}/{filePath}";
            }
            catch (Exception)
            {
                throw new FShopNotImplementedException();
            }
        }

        public async Task SaveFileAsync(Stream mediaBinaryStream, string filePath)
        {
            try
            {
                var fileName = Path.Combine(_userContentFolder, filePath);
                using var output = new FileStream(fileName, FileMode.Create);
                await mediaBinaryStream.CopyToAsync(output);
            }
            catch (Exception)
            {
                throw new FShopNotImplementedException();
            }
        }
    }
}
