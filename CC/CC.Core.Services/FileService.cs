using CC.Core.Models;
using CC.Core.Models.IO;
using CC.Core.Services.Interfaces;
using System;
using System.IO;
using System.Text;

namespace CC.Core.Services
{
    public sealed class FileService : IFileService
    {
        public TextReader ReadText(IFile file)
        {
            return File.OpenText(file.Path);
        }

        public void WriteText(IFile file, string text)
        {
            File.WriteAllText(file.Path, text);
        }
    }
}
