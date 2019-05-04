using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageServe.WebModels.BindingModels
{
    public class ImageUploadBindingModel
    {
        public IFormFile File { get; set; }

        public string Description { get; set; }

        public bool IsPublic { get; set; }

        //TODO
        //public bool SetAsAvatar { get; set; }
    }
}
