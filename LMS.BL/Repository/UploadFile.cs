using LMS.BL.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BL.Repository
{
    public class UploadFile : IUploadFile
    {
        private readonly IHostingEnvironment _hostingEnv;

        public UploadFile(IHostingEnvironment hostingEnv)
        {
            _hostingEnv = hostingEnv;
        }
        public async Task<string> UploadFileServices(IFormFile file, string path)
        {
            var newFullPath = string.Empty;

            if (file != null)
            {
                try
                {
                    var data = _hostingEnv.WebRootPath;
                    string FilePath = Path.Combine(_hostingEnv.WebRootPath, path);
                    if (!Directory.Exists(FilePath))
                    {
                        Directory.CreateDirectory(FilePath);
                    }

                    using (FileStream filestream = File.Create($"{_hostingEnv.WebRootPath}\\{path}\\{file.FileName}"))
                    {
                        await file.CopyToAsync(filestream);
                        await filestream.FlushAsync();
                        newFullPath = $"\\{path}\\{file.FileName}";

                    }
                }
                catch (Exception ex)
                {

                }
            }
            else
            {

            }

            return newFullPath;

        }
    
    }
}
