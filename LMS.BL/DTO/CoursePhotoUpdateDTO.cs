﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BL.DTO
{
    public class CoursePhotoUpdateDTO
    {
        public int Id { get; set; }
        public string? UserAttachmentPath { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
