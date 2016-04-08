using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.Win32;
using System.Xml;
using FirstFloor.ModernUI.Windows.Controls;
using System.IO;

namespace Crossword_Application_Modern
{
    /// <summary>
    /// Класс, предназначенный для сохранения и загрузки файлов сетки кроссворда (cwgf).
    /// (Устарел. Будет удален с завершением работы над новым редактором сетки и функцией заполнения)
    /// </summary>
    class GridXmlHandler
    {
        /// <summary>
        /// Имя файла.
        /// </summary>
        private string fileName;

        /// <summary>
        /// Путь к файлу.
        /// </summary>
        private string filePath;

        /// <summary>
        /// Коллекция <see cref="GridWord"/>, заполняемая во время операции чтения.
        /// </summary>
        private List<GridWord> readedWords = new List<GridWord>();

        /// <summary>
        /// Полученная коллекция <see cref="GridWord"/>.
        /// </summary>
        private List<GridWord> inputWords;

        /// <summary>
        /// Указывает на наличие полученной коллекции <see cref="GridWord"/>.
        /// </summary>
        private bool hasInputWords = false;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="GridXmlHandler"/>.
        /// </summary>
        public GridXmlHandler()
        { Reset(); }

        /// <summary>
        /// Получает коллецию слов, которую требуется сохранить.
        /// </summary>
        /// <param name="_input">Коллекция слов <seealso cref="GridWord"/>.</param>
        public void SetInputWords(List<GridWord> _input)
        { 
            inputWords = _input;
            hasInputWords = true;
        }

        /// <summary>
        /// Запускает цепочку вызовов для чтения файла по пути, который получает как результат работы диалога открытия.
        /// </summary>
        /// <returns>Успех операции открытия.</returns>
        public bool OpenFileFromDialog()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Открыть файл сетки кроссворда";
            openFileDialog1.Filter = "Crossword Creator Grid File|*.cwgf";            
            openFileDialog1.DefaultExt = "cwgf";
            openFileDialog1.Multiselect = false;
            openFileDialog1.RestoreDirectory = true;

            BlurHandler.Blur(true);
            if (openFileDialog1.ShowDialog() == true)
            {
                BlurHandler.Blur(false);
                filePath = openFileDialog1.FileName;
                fileName = Path.GetFileNameWithoutExtension(filePath);

                if (ReadXml() == true)
                { return true; }
                else
                {
                    filePath = String.Empty;
                    fileName = String.Empty;
                    return false;
                }
            }      
            else
            {
                BlurHandler.Blur(false);
                return false; 
            }            
        }

        /// <summary>
        /// Запускает цепочку вызово для чтения файла по указанному пути.
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        /// <returns>Успех операции.</returns>
        public bool OpenFileFromPath(string path)
        {
            filePath = path;
            fileName = Path.GetFileNameWithoutExtension(path);

            if (ReadXml() == true)
            { return true; }
            else
            {
                filePath = String.Empty;
                fileName = String.Empty;
                return false;
            }
        }

        /// <summary>
        /// Запускает цепочку вызовов для сохранения сетки кроссворда по пути, который получает как результат отображения окна сохранения или через переданный параметр.
        /// </summary>
        /// <param name="path">Путь, по которому требуется сохранить коллекцию. Передать <see cref="String.Empty"/>, если требуется сохранить в новый файл.</param>
        /// <returns>Успех операции осхранения.</returns>
        public bool SaveFile(string path)
        {
            filePath = path;

            if (hasInputWords == true)
            {
                if (filePath == String.Empty)
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.RestoreDirectory = true;
                    saveFileDialog1.DefaultExt = "cwgf";
                    saveFileDialog1.Filter = "Crossword Creator Grid File|*.cwgf|Все файлы|*.*";
                    saveFileDialog1.Title = "Сохранить файл сетки кроссворда";

                    BlurHandler.Blur(true);
                    if (saveFileDialog1.ShowDialog() == true)
                    {
                        BlurHandler.Blur(false);
                        fileName = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);
                        filePath = saveFileDialog1.FileName;

                        if (CreateNewXml() == true)
                        {
                            if (WriteToXmlFile() == true)
                            { return true; }
                            else
                            { return false; }
                        }
                        else
                        { return false; }
                    }
                    else
                    {
                        BlurHandler.Blur(false);
                        return false; 
                    }
                }
                else
                {
                    if (CreateNewXml() == true)
                    {
                        if (WriteToXmlFile() == true)
                        {
                            fileName = Path.GetFileNameWithoutExtension(filePath);
                            return true; 
                        }
                        else
                        { return false; }
                    }
                    else
                    { return false; }
                }
            }   
            else
            { return false; }
        }

        /// <summary>
        /// Выполняет чтение файла сетки кроссворда, сохраняя результат в коллекцию <seealso cref="readedWords"/>.
        /// </summary>
        /// <returns>Успех операции чтения.</returns>
        private bool ReadXml()
        {
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(filePath);

                foreach (XmlElement element in document.GetElementsByTagName("gridWord"))
                {
                    int ID = 0;
                    int X = 0;
                    int Y = 0;
                    Orientation orientation = Orientation.Vertical;
                    string word = String.Empty;
                    string question = String.Empty;

                    foreach (XmlElement subElement in element)
                    {                        
                        if (subElement.Name == "ID")
                        { ID = int.Parse(subElement.InnerText); }
                        else if (subElement.Name == "X")
                        { X = int.Parse(subElement.InnerText); }
                        else if (subElement.Name == "Y")
                        { Y = int.Parse(subElement.InnerText); }
                        else if (subElement.Name == "orientation")
                        {
                            switch (subElement.InnerText)
                            {
                                case "Horizontal":
                                    orientation = Orientation.Horizontal;
                                    break;
                                case "Vertical":
                                    orientation = Orientation.Vertical;
                                    break;
                            }
                        }
                        else if (subElement.Name == "answer")
                        { word = subElement.InnerText; }
                        else if (subElement.Name == "question")
                        { question = subElement.InnerText; }
                    }

                    readedWords.Add(new GridWord(word, question, ID,
                        X, Y, orientation));
                }

                return true;
            }
            catch(Exception ex)
            {       
                Messages.OpenFileError(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Возвращает коллекцию <seealso cref="GridWord"/> с прочитанными данными.
        /// </summary>
        public List<GridWord> GetReadedWords
        {
            get { return readedWords; }
        }

        /// <summary>
        /// Возвращает имя файла.
        /// </summary>
        public string GetFileName
        {
            get { return fileName; }
        }

        /// <summary>
        /// Возвращает путь к файлу.
        /// </summary>
        public string GetFilePath
        {
            get { return filePath; }
        }

        /// <summary>
        /// Создает новый файл сетки кроссворда.
        /// </summary>
        /// <returns>Успех операции создания.</returns>
        private bool CreateNewXml()
        {
            try
            {
                XmlTextWriter xmlTextWritter =
                new XmlTextWriter(filePath, Encoding.UTF8);

                xmlTextWritter.WriteStartDocument();
                xmlTextWritter.WriteStartElement("head");
                xmlTextWritter.WriteEndElement();
                xmlTextWritter.Close();
                
                return true;
            }
            catch(Exception ex)
            {
                Messages.SaveFileError(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Выполняет запись данных в файл сетки кроссворда.
        /// </summary>
        /// <returns>Успех операции записи.</returns>
        private bool WriteToXmlFile()
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(filePath);

                for (int i = 0; i < inputWords.Count; i++)
                {
                    XmlNode gridWord = document.CreateElement("gridWord");
                    document.DocumentElement.AppendChild(gridWord);

                    XmlNode wordID = document.CreateElement("ID");
                    wordID.InnerText = Convert.ToString(inputWords[i].GetID);
                    gridWord.AppendChild(wordID);

                    XmlNode wordPosX = document.CreateElement("X");
                    wordPosX.InnerText = Convert.ToString(inputWords[i].X);
                    gridWord.AppendChild(wordPosX);

                    XmlNode wordPosY = document.CreateElement("Y");
                    wordPosY.InnerText = Convert.ToString(inputWords[i].Y);
                    gridWord.AppendChild(wordPosY);

                    XmlNode wordOrientation = document.CreateElement("orientation");
                    wordOrientation.InnerText = inputWords[i].Orientation.ToString();
                    gridWord.AppendChild(wordOrientation);

                    XmlNode answer = document.CreateElement("answer");
                    answer.InnerText = inputWords[i].Word;
                    gridWord.AppendChild(answer);

                    XmlNode question = document.CreateElement("question");
                    question.InnerText = inputWords[i].Question;
                    gridWord.AppendChild(question);
                }

                document.Save(filePath);
                return true;
            }
            catch(Exception ex)
            {
                Messages.SaveFileError(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Восстанавливает все параметры <see cref="GridXmlHandler"/> на параметры по умолчанию.
        /// </summary>
        private void Reset()
        {
            fileName = String.Empty;
            filePath = String.Empty;
            readedWords.Clear();
            hasInputWords = false;
        }
    }
}
