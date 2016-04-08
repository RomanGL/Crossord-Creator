using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml;
using System.IO;
using Crossword_Application_Modern.Model;

namespace Crossword_Application_Modern.Handlers
{
    /// <summary>
    /// Статический класс, предназначенный для сохранения и загрузки файлов сетки кроссворда (cwgf).
    /// </summary>
    public static class GridWordXmlHandler
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
        /// Выполняет сохранение коллекции <see cref="GridWordViewModel"/> в Xml-файл по указанному пути.
        /// </summary>
        /// <param name="items">Коллекция <see cref="GridWordViewModel"/>, которую требуется сохранить.</param>
        /// <returns>Успех операции сохранения.</returns>
        public static bool Save(string filePath, List<GridWordModel> items)
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

                foreach (GridWordModel item in items)
                {
                    XmlNode gridWord = document.CreateElement("gridWord");
                    document.DocumentElement.AppendChild(gridWord);

                    XmlNode wordID = document.CreateElement("ID");
                    wordID.InnerText = Convert.ToString(item.ID);
                    gridWord.AppendChild(wordID);

                    XmlNode Xpos = document.CreateElement("X");
                    Xpos.InnerText = Convert.ToString(item.X);
                    gridWord.AppendChild(Xpos);

                    XmlNode Ypos = document.CreateElement("Y");
                    Ypos.InnerText = Convert.ToString(item.Y);
                    gridWord.AppendChild(Ypos);

                    XmlNode orientation = document.CreateElement("orientation");
                    orientation.InnerText = item.Orientation.ToString();
                    gridWord.AppendChild(orientation);

                    XmlNode answer = document.CreateElement("answer");
                    answer.InnerText = item.Answer;
                    gridWord.AppendChild(answer);

                    XmlNode question = document.CreateElement("question");
                    question.InnerText = item.Question;
                    gridWord.AppendChild(question);
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
        /// <returns>Коллекция <see cref="GridWordViewModel"/> из считанного файла.</returns>
        public static List<GridWordModel> Open(string filePath, out bool success)
        {
            _filePath = filePath;
            _fileName = Path.GetFileNameWithoutExtension(_filePath);
            success = false;

            // Коллекция, в которую будут записаны считанные данные.
            List<GridWordModel> readed = new List<GridWordModel>();

            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(filePath);

                foreach (XmlElement element in document.GetElementsByTagName("gridWord"))
                {
                    int _id = 0;
                    double _x = 0;
                    double _y = 0;
                    Orientation _orientation = Orientation.Vertical;
                    string _answer = String.Empty;
                    string _question = String.Empty;

                    foreach (XmlElement subElement in element)
                    {
                        if (subElement.Name == "ID")
                        { _id = int.Parse(subElement.InnerText); }
                        else if (subElement.Name == "X")
                        { _x = int.Parse(subElement.InnerText); }
                        else if (subElement.Name == "Y")
                        { _y = int.Parse(subElement.InnerText); }
                        else if (subElement.Name == "orientation")
                        {
                            switch (subElement.InnerText)
                            {
                                case "Horizontal":
                                    _orientation = Orientation.Horizontal;
                                    break;
                                case "Vertical":
                                    _orientation = Orientation.Vertical;
                                    break;
                            }
                        }
                        else if (subElement.Name == "answer")
                        { _answer = subElement.InnerText; }
                        else if (subElement.Name == "question")
                        { _question = subElement.InnerText; }
                    }
                    // Добавление слова в коллекцию.
                    readed.Add(new GridWordModel() 
                    { 
                        Answer = _answer, 
                        Question = _question, 
                        ID = _id,
                        X = _x,
                        Y = _y,
                        Orientation = _orientation
                    });
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
