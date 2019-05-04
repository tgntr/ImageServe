using ImageServe.Models;
using ImageServe.WebModels.BindingModels;
using ImageServe.WebModels.Dtos;
using ImageServe.WebModels.ViewModels;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServe.Services.Contracts
{
    public interface IImageService
    {
        Task AddAsync(ImageUploadBindingModel uploadBindingModel);

        ICollection<ImageDto> AllByUser(string userId);

        Task<T> GetImageAsync<T>(int imageId);

        IPagedList<ImageViewModel> Newest(int pageNumber, int pageSize);

        Task EditAsync(ImageEditBindingModel image);

        Task<bool> ToggleLikeAsync(string userId, int imageId);

        int GetLikesCount(int imageId);
         
        bool IsLiked(string userId, int imageId);

        Task RemoveAsync(int imageId);

        Task AddComment(CommentViewModel vm);

        ICollection<ImageDto> Search(string query, int page, int pageSize);

        //bool Exists(int id);
        //
        //TModel ById<TModel>(int id);

    }
}
