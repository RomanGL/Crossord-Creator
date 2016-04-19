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
using CC.Core.Models.IO;
using System.IO;

namespace CC.Core.Services
{
    public sealed class FileInfoService : IFileInfoService
    {
        public FileInfoService(IZipFileService zipFileService)
        {

        }

        public CCFileInfo GetFileInfo(IFile file)
        {
            try
            {
                var info = new CCFileInfo();
                info.Name = file.Name;
                info.Path = file.Path;

                var streamReader = new StreamReader(file.Path);
                char fChar = (char)streamReader.Peek();

                if (fChar == '<' && file.Name.EndsWith(".cwtf"))
                {
                    info.Type = CCFileType.cwtf;
                    info.Version = new CCVersion("Crossword Creator 1.x");
                    return info;
                }
                else if (fChar == '<' && file.Name.EndsWith(".cwgf"))
                {
                    info.Type = CCFileType.cwgf;
                    info.Version = new CCVersion("Crossword Creator 1.x");
                    return info;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                throw new CCFileException(file, "Не удалось обработать файл.", ex);
            }
        }

        private CCVersion GetXmlVersion(StreamReader reader)
        {
            var document = XDocument.Load(reader);
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

        private CCVersion GetJsonVersion(StreamReader reader)
        {
            using (var jReader = new JsonTextReader(reader))
            {
                var obj = JToken.ReadFrom(jReader);
                var version = obj["AppVersion"].ToObject<CCVersion>();
                return version;
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
