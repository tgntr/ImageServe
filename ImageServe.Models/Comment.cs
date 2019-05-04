using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ImageServe.Models
{
    public  class Comment
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public string UserName { get; set; }

        

    }
}
