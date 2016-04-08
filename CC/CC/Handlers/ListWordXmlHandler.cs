using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Documents;
using System.Xml;
using System.IO;
using Crossword_Application_Modern.Model;

namespace Crossword_Application_Modern.Handlers
{
    /// <summary>
    /// Класс, предназначенный для сохранения и загрузки файлов списков треминов (cwtf).
    /// </summary>
    public static class ListWordXmlHandler
    {
        private static string _filePath;
        private static string _fileName;

        /// <summary>
        /// Возвращает имя файла без пути.
        /// </summary>
        public static string FileName
        { get { return _fileName; } }

        /// <summary>
        /// Возвращает полный путь файла.
        /// </summary>
        public static string FilePath
        { get { return _filePath; } }

        /// <summary>
        /// Выполняет сохранение коллекции <see cref="ListWordModel"/> в Xml-файл по указанному пути.
        /// </summary>
        /// <param name="items">Коллекция <see cref="ListWordModel"/>, которую требуется сохранить.</param>
        /// <returns>Успех операции сохранения.</returns>
        public static bool Save(string filePath, List<ListWordModel> items)
        {
            try
            {
                _filePath = filePath;
                _fileName = Path.GetFileNameWithoutExtension(_filePath);

                // Создание нового Xml-файла.
                XmlTextWriter xmlTextWritter =
                new XmlTextWriter(_filePath, Encoding.UTF8);

                xmlTextWritter.WriteStartDocument();
                xmlTextWritter.WriteStartElement("head");
                xmlTextWritter.WriteEndElement();
                xmlTextWritter.Close();

                // Запись в созданный Xml-файл.
                XmlDocument document = new XmlDocument();
                document.Load(_filePath);

                foreach (ListWordModel item in items)
                {
                    XmlNode word = document.CreateElement("word");
                    document.DocumentElement.AppendChild(word);

                    XmlNode wordID = document.CreateElement("ID");
                    wordID.InnerText = Convert.ToString(item.ID);
                    word.AppendChild(wordID);

                    XmlNode answer = document.CreateElement("answer");
                    answer.InnerText = item.Answer;
                    word.AppendChild(answer);

                    XmlNode question = document.CreateElement("question");
                    question.InnerText = item.Question;
                    word.AppendChild(question);
                }

                document.Save(_filePath);
                return true;
            }
            catch (Exception ex)
            {
                Messages.SaveFileError(ex.Message);
                return false;
            }            
        }

        /// <summary>
        /// Выполняет чтение и разбор файла списка терминов.
        /// </summary>
        /// <param name="filePath">Путь к файлу.</param>
        /// <returns>Коллекция <see cref="ListWordModel"/> из считанного файла.</returns>
        public static List<ListWordModel> Open(string filePath, out bool success)
        {
            _filePath = filePath;
            _fileName = Path.GetFileNameWithoutExtension(_filePath);
            success = false;

            // Коллекция, в которую будут записаны считанные данные.
            List<ListWordModel> readed = 
                new List<ListWordModel>();
            
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(filePath);

                foreach (XmlElement element in document.GetElementsByTagName("word"))
                {
                    int _id = 0;
                    string _answer = String.Empty;
                    string _question = String.Empty;

                    foreach (XmlElement subElement in element)
                    {
                        if (subElement.Name == "ID")
                        { _id = int.Parse(subElement.InnerText); }
                        else if (subElement.Name == "answer")
                        { _answer = subElement.InnerText; }
                        else if (subElement.Name == "question")
                        { _question = subElement.InnerText; }
                    }
                    // Добавление слова в коллекцию.
                    readed.Add(new ListWordModel() { Answer = _answer, Question = _question, ID = _id });
                }
                success = true;
            }
            catch (Exception ex)
            {
                Messages.OpenFileError(ex.Message);
                success = false;
            }

            return readed;
        }
    }
}
