using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CC.Core.Model;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Xml.Linq;

namespace CC.Core.Services
{
    /// <summary>
    /// Представляет сервис для работы со списками терминов в формате XML.
    /// </summary>
    public class XmlListService : IListService, IFileInfoService
    {
        private const string APP_VERSION_ELEMENT_NAME = "applicationVersion";
        private const string APP_NAME_ELEMENT_NAME = "aplicationName";
        private const string VERSION_MAJOR_ELEMENT_NAME = "major";
        private const string VERSION_MINOR_ELEMENT_NAME = "minor";
        private const string VERSION_BUILD_ELEMENT_NAME = "build";
        private const string VERSION_REVISION_ELEMENT_NAME = "revision";

        private const string LIST_ELEMENT_NAME = "list";
        private const string HEAD_ELEMENT_NAME = "head";
        private const string WORD_ELEMENT_NAME = "word";
        private const string ID_ELEMENT_NAME = "ID";
        private const string ANSWER_ELEMENT_NAME = "answer";
        private const string QUESTION_ELEMENT_NAME = "question";

        /// <summary>
        /// Возвращает версию файла Crossword Creator.
        /// </summary>
        /// <param name="file">Файл.</param>
        /// <param name="service">Сервис для работы с файлами.</param>
        /// <exception cref="CCFileException">Выбрасывается при невозможности прочитать или обработать файл.</exception>
        public CCVersion GetFileInfo(IFile file, IFileService service)
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

        /// <summary>
        /// Возвращает список терминов из текстовой строки.
        /// </summary>
        /// <param name="file">Файл.</param>
        /// <param name="service">Сервис для работы с файлами.</param>
        /// <exception cref="CCFileException">Выбрасывается при невозможности прочитать или обработать файл.</exception>
        public List<ListWord> GetListFromFile(IFile file, IFileService service)
        {
            try
            {
                var document = XDocument.Load(service.ReadText(file));
                var list = new List<ListWord>();

                foreach (XElement child in document.Root.Elements(XName.Get(WORD_ELEMENT_NAME)))
                {
                    var word = new ListWord();
                    word.ID = int.Parse(child.Element(XName.Get(ID_ELEMENT_NAME)).Value);
                    word.Answer = child.Element(XName.Get(ANSWER_ELEMENT_NAME)).Value;
                    word.Question = child.Element(XName.Get(QUESTION_ELEMENT_NAME)).Value;
                    list.Add(word);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new CCFileException(file, "Не удалось обработать файл.", ex);
            }
        }

        /// <summary>
        /// Сохраняет список терминов в файл.
        /// </summary>
        /// <param name="list">Список для сохранения.</param>
        /// <param name="version">Версия приложения, создавшего список.</param>
        /// <param name="file">Файл, в который требуется записать список.</param>
        /// <param name="service">Сервис для работы с файлами.</param>
        /// <exception cref="CCFileException">Выбрасывается при невозможности сериализовать 
        /// список или записать данные в файл.</exception>
        public void SaveListFile(IEnumerable<ListWord> list, CCVersion version, IFile file, IFileService service)
        {
            try
            {
                var document = new XDocument();                

                var headElement = new XElement(XName.Get(HEAD_ELEMENT_NAME));
                var versionElement = new XElement(XName.Get(APP_VERSION_ELEMENT_NAME));

                versionElement.Add(new XElement(XName.Get(APP_NAME_ELEMENT_NAME), version.ApplicationName));
                versionElement.Add(new XElement(XName.Get(VERSION_MAJOR_ELEMENT_NAME), version.Major));
                versionElement.Add(new XElement(XName.Get(VERSION_MINOR_ELEMENT_NAME), version.Minor));
                versionElement.Add(new XElement(XName.Get(VERSION_BUILD_ELEMENT_NAME), version.Build));
                versionElement.Add(new XElement(XName.Get(VERSION_REVISION_ELEMENT_NAME), version.Revision));

                headElement.Add(versionElement);

                var listElement = new XElement(XName.Get(LIST_ELEMENT_NAME));

                foreach (var word in list)
                {
                    var wordElement = new XElement(XName.Get(WORD_ELEMENT_NAME));

                    wordElement.Add(new XElement(XName.Get(ID_ELEMENT_NAME), word.ID));
                    wordElement.Add(new XElement(XName.Get(ANSWER_ELEMENT_NAME), word.Answer));
                    wordElement.Add(new XElement(XName.Get(QUESTION_ELEMENT_NAME), word.Question));

                    listElement.Add(wordElement);
                }

                headElement.Add(listElement);
                document.Add(headElement);

                var writer = new Utf8StringWriter();
                document.Save(writer);

                string text = writer.ToString();
                service.WriteText(file, text);
            }
            catch (Exception ex)
            {
                throw new CCFileException(file, "Не удалось сохранить файл.", ex);
            }
        }
    }
}
