using ImageServe.WebModels.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ImageServe.WebModels.ViewModels
{
    public class SearchViewModel
    {
        [Required]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateUploaded { get; set; }

        public UserDto User { get; set; }

        public string GetThumbUrl() => "https://netacademy.blob.core.windows.net/thumbs/" + this.Name + "-thumb.jpg";
    }
}
