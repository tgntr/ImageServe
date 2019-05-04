using AutoMapper;
using ImageServe.Models;
using ImageServe.WebModels.Dtos;
using ImageServe.WebModels.ViewModels;
using ImageServe.WebModels.BindingModels;

namespace ImageServe
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<Image, ImageDto>()
                .ForMember(dto=>dto.LikesCount, map=> map.MapFrom(i=>i.Likes.Count));
            CreateMap<User, UserDto>();
            CreateMap<ImageTag, TagDto>();
            CreateMap<Image, ImageViewModel>();
            CreateMap<User, UserViewModel>();
            //CreateMap<Image, SearchImageViewModel>();

            CreateMap<Friendship, FriendshipDto>()
                .ForMember(dto => dto.FriendFullName, map => map.MapFrom(f => f.Friend.GetFullName()))
                .ForMember(dto => dto.FriendAvatar, map => map.MapFrom(f => f.Friend.Avatar));
            CreateMap<User, UserFriendlistViewModel>();
            CreateMap<User, UserEditViewModel>(); //Must be Binding model 
            CreateMap<Image, ImageEditBindingModel>();

        }

    }
}
