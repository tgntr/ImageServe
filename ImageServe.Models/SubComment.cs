using System;
using System.Collections.Generic;
using System.Text;

namespace ImageServe.Models
{
    public class SubComment : Comment
    {
        public int MainCommentId { get; set; }

        public virtual MainComment MainComment { get; set; }
    }
}
