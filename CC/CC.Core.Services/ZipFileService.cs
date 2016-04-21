using CC.Core.Models.IO;
using CC.Core.Services.Interfaces;
using ICSharpCode.SharpZipLib.Zip;
using Newtonsoft.Json;
using System;
using System.IO;

namespace CC.Core.Services
{
    public sealed class ZipFileService : IZipFileService
    {
        public CCFileInfo GetFileInfo(IFile file)
        {
            ZipFile ccZip = null;
            try
            {
                ccZip = new ZipFile(file.Path);
                ZipEntry versionEntry = ccZip.GetEntry(VersionFileName);
                var streamReader = new StreamReader(ccZip.GetInputStream(versionEntry));

                var jsonSerializer = new JsonSerializer();
                var info = (CCFileInfo)jsonSerializer.Deserialize(streamReader, typeof(CCFileInfo));
                
                return info;
            }
            catch (Exception ex)
            {
                throw new CCFileException(file, "Файл поврежден либо имеет неизвестный формат.", ex);
            }
            finally
            {
                ccZip?.Close();
            }
        }

        private const string VersionFileName = "cc.ver";
    }
}
