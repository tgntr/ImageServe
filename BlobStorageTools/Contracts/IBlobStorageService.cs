using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BlobStorageTools.Contracts
{
    public interface IBlobStorageService
    {
        Task UploadFromFileAsync(byte[] file, string name, string extension);


        Task DeleteImageAsync(string imageName);
    }
}
