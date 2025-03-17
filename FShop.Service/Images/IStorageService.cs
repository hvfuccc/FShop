using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FShop.Service.Images
{
    public interface IStorageService
    {
        string GetFileUrl(string filePath);
        Task SaveFileAsync(Stream mediaBinaryStream, string filePath);
        Task DeleteFileAsync(string filePath);
    }
}
