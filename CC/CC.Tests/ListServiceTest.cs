using CC.Tests.Core.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CC.Core.Services;
using CC.Core.Model;
using CC.Tests.Core.Service;

namespace CC.Tests
{
    /// <summary>
    /// Содержит тесты для сервисов, реализующих <see cref="IListService"/>.
    /// </summary>
    [TestClass]
    public class ListServiceTest
    {
        /// <summary>
        /// Записывает тестовый файл списка терминов в формате XML.
        /// </summary>
        [TestMethod]
        public void WriteXmlListFile()
        {
            var version = new CCVersion("CC Unit Tester", 1, 0, 0, 0);

            var list = new List<ListWord>
            {
                new ListWord { ID = 1, Question = "Вопрос?", Answer = "ответ" },
                new ListWord { ID = 2, Question = "Вопрос?", Answer = "ответ" },
                new ListWord { ID = 3, Question = "Вопрос?", Answer = "ответ" },
                new ListWord { ID = 4, Question = "Вопрос?", Answer = "ответ" },
                new ListWord { ID = 5, Question = "Вопрос?", Answer = "ответ" }
            };

            IListService listService = new XmlListService();
            IFileService fileService = new FileService();
            IFile file = new File
            {
                Name = "ResultXml.cwtf",
                Path = "ResultXml.cwtf"
            };

            listService.SaveListFile(list, version, file, fileService);
        }

        /// <summary>
        /// Считывает тестовый файл списка терминов в формате XML.
        /// </summary>
        [TestMethod]
        public void ReadXmlListFile()
        {
            IListService listService = new XmlListService();
            IFileService fileService = new FileService();
            IFile file = new File
            {
                Name = "TestXml.cwtf",
                Path = @"Assets\TestXml.cwtf"
            };

            List<ListWord> words = listService.GetListFromFile(file, fileService);
        }

        /// <summary>
        /// Записывает тестовый файл списка терминов в формате JSON.
        /// </summary>
        [TestMethod]
        public void WriteJsonListFile()
        {
            var version = new CCVersion("CC Unit Tester", 1, 0, 0, 0);

            var list = new List<ListWord>
            {
                new ListWord { ID = 1, Question = "Вопрос?", Answer = "ответ" },
                new ListWord { ID = 2, Question = "Вопрос?", Answer = "ответ" },
                new ListWord { ID = 3, Question = "Вопрос?", Answer = "ответ" },
                new ListWord { ID = 4, Question = "Вопрос?", Answer = "ответ" },
                new ListWord { ID = 5, Question = "Вопрос?", Answer = "ответ" }
            };

            IListService listService = new JsonListService();
            IFileService fileService = new FileService();
            IFile file = new File
            {
                Name = "ResultJson.cwtf",
                Path = "ResultJson.cwtf"
            };

            listService.SaveListFile(list, version, file, fileService);
        }

        /// <summary>
        /// Считывает тестовый файл списка терминов в формате JSON.
        /// </summary>
        [TestMethod]
        public void ReadJsonListFile()
        {
            IListService listService = new JsonListService();
            IFileService fileService = new FileService();
            IFile file = new File
            {
                Name = "TestJson.cwtf",
                Path = @"Assets\TestJson.cwtf"
            };

            List<ListWord> words = listService.GetListFromFile(file, fileService);
        }
    }
}
