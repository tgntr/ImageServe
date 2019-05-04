using System;
using System.Collections.Generic;
using System.Text;

namespace ImageServe.Models
{
    public class MainComment: Comment
    {
        public int ImageId { get; set; }
        public virtual Image Image { get; set; }
        public virtual List<SubComment> SubComments { get; set; }
    }
}
