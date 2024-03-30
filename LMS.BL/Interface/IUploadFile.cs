using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BL.Interface
{
    public interface IUploadFile
    {
        Task<string> UploadFileServices(IFormFile file, string path);
    }
}
