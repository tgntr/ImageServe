using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ImageServe.WebModels.ViewModels
{
    public class CommentViewModel
    {
        [Required]
        public int ImageId { get; set; }

        [Required]
        public int MainCommentId { get; set; }

        [Required]
        public string Message { get; set; }

        
        
    }
}
