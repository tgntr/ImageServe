using System;
using System.Collections.Generic;
using System.Text;

namespace ImageServe.WebModels.Dtos
{
    public class ImageDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public UserDto User { get; set; }

        public string LikesCount { get; set; }

        public DateTime DateUploaded { get; set; }

        public bool IsPublic { get; set; }
    }
}
