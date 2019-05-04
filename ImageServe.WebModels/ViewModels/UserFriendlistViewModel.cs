using ImageServe.WebModels.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageServe.WebModels.ViewModels
{
    public class UserFriendlistViewModel
    {
        public string Id { get; set; }

        public  ICollection<FriendshipDto> UserFriends { get; set; }

        public string AddFriend { get; set; }
    }
}
