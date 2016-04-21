using CC.Core.Models;
using CC.Core.Models.IO;
using CC.Core.Services.Interfaces;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CC.Core.Services
{
    public sealed class FileService : IFileService
    {
        public FileService(IBlurService blurService)
        {
            _blurService = blurService;
        }

        public string OpenGridFile()
        {
            OpenFile();
            return null;
        }

        public string OpenListFile()
        {
            OpenFile();
            return null;
        }

        private async void OpenFile()
        {
            _blurService.Blur();
            await Task.Delay(5000);
            _blurService.UnBlur();
        }

        private readonly IBlurService _blurService;
    }
}
