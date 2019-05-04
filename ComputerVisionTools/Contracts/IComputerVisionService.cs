using ImageServe.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComputerVisionTools.Contracts
{
    public interface IComputerVisionService
    {
        Task<ICollection<ImageTag>> GenerateTagsAsync(string imageUrl);
    }
}
