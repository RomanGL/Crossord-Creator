using CC.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CC.Core.Models;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace CC.Core.Services
{
    public sealed class FileInfoService : IFileInfoService
    {
        public CCFileFormat GetFileFormat(IFile file, IFileService service)
        {
            throw new NotImplementedException();
        }

        public CCVersion GetFileInfo(IFile file, IFileService service)
        {
            throw new NotImplementedException();
        }

        private CCVersion GetXmlVersion(IFile file, IFileService service)
        {
            try
            {
                var document = XDocument.Load(service.ReadText(file));
                XElement versionElement = document.Root.Element(XName.Get(APP_VERSION_ELEMENT_NAME));

                if (versionElement == null) return new CCVersion();
                var version = new CCVersion();

                version.ApplicationName = versionElement.Element(XName.Get(APP_NAME_ELEMENT_NAME)).Value;
                version.Major = uint.Parse(versionElement.Element(XName.Get(VERSION_MAJOR_ELEMENT_NAME)).Value);
                version.Minor = uint.Parse(versionElement.Element(XName.Get(VERSION_MINOR_ELEMENT_NAME)).Value);
                version.Build = uint.Parse(versionElement.Element(XName.Get(VERSION_BUILD_ELEMENT_NAME)).Value);
                version.Revision = uint.Parse(versionElement.Element(XName.Get(VERSION_REVISION_ELEMENT_NAME)).Value);

                return version;
            }
            catch (Exception ex)
            {
                throw new CCFileException(file, "Не удалось обработать файл.", ex);
            }
        }

        private CCVersion GetJsonVersion(IFile file, IFileService service)
        {
            try
            {
                using (var reader = new JsonTextReader(service.ReadText(file)))
                {
                    var obj = JToken.ReadFrom(reader);
                    var version = obj["AppVersion"].ToObject<CCVersion>();
                    return version;
                }
            }
            catch (Exception ex)
            {
                throw new CCFileException(file, "Не удалось обработать файл.", ex);
            }
        }

        private const string APP_VERSION_ELEMENT_NAME = "applicationVersion";
        private const string APP_NAME_ELEMENT_NAME = "aplicationName";
        private const string VERSION_MAJOR_ELEMENT_NAME = "major";
        private const string VERSION_MINOR_ELEMENT_NAME = "minor";
        private const string VERSION_BUILD_ELEMENT_NAME = "build";
        private const string VERSION_REVISION_ELEMENT_NAME = "revision";
    }
}
