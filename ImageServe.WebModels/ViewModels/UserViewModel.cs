using ImageServe.WebModels.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImageServe.WebModels.ViewModels
{
    public class UserViewModel
    {
        [Required]
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Details { get; set; }

        public string Avatar { get; set; }
        
        public string Country { get; set; }

        public string City { get; set; }

        public IEnumerable<ImageDto> Images { get; set; }

        public string GetFullName()
        {
            return FirstName + " " + LastName;
        }
    }
}
