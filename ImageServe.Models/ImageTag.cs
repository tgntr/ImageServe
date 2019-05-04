﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ImageServe.Models
{
    public class ImageTag
    {
        public string Name { get; set; }

        public bool FromDescription { get; set; }

        public int ImageID { get; set; }
        public virtual Image Image { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int Id { get; set; }

        //[Required]
        //public int TagId { get; set; }
        //public Tag Tag { get; set; }
    }
}
