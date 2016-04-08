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
using System.Windows.Forms;
using System.Xml;
using FirstFloor.ModernUI.Windows.Controls;
using System.IO;

namespace Crossword_Application_Modern
{
    /// <summary>
    /// Класс, предназначенный для сохранения и загрузки файлов списка терминов (cwtf).
    /// (Устарел. Будет удален с завершением работы над новым редактором сетки и функцией заполнения)
    /// </summary>
    class XmlHandler
    {
        /// <summary>
        /// Возвращает путь файла.
        /// </summary>
        public string filePath { get; private set; }

        /// <summary>
        /// Возвращает имя файла.
        /// </summary>
        public string fileName { get; private set; }

        /// <summary>
        /// Возвращает или задает количество терминов в коллекции.
        /// </summary>
        public int wordsCount;

        /// <summary>
        /// Возвращает коллекцию терминов, которую удалось прочитать.
        /// </summary>
        public List<EditorWord> readedWords { get; private set; }

        /// <summary>
        /// Возвращает или задает полученную коллекцию терминов.
        /// </summary>
        private ObservableCollection<EditorWord> inputWords;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="XmlHandler"/> с заданным путем файла.
        /// </summary>
        /// <param name="path">Путь файла.</param>
        public XmlHandler(string path)
        {
            Reset();
            filePath = path; 
        }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="XmlHandler"/>.
        /// </summary>
        public XmlHandler() 
        {
            Reset();
        }

        /// <summary>
        /// Запускает цепочку вызовов для открытия файла списка терминов по пути, который получает как результат отображения диалога открытия.
        /// </summary>
        /// <param name="success">Успех операции открытия.</param>
        public void OpenFile(out bool success)
        {            
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = false;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "Открыть файл списка определений";
            openFileDialog1.Filter = "Crossword Creator List File (*.cwtf)|*.cwtf";

            BlurHandler.Blur(true);
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
	        {
                filePath = openFileDialog1.FileName;
                fileName = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);

                if (ReadXml() == true)
                { success = true; }
                else
                { success = false; }
            }
            else
            {
                success = false;
            }
            BlurHandler.Blur(false);
            openFileDialog1.Dispose();
        }

        /// <summary>
        /// Запускает цепочку вызовов для открытия файла списков терминов по определенному пути.
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        /// <returns>Успех операции открытия.</returns>
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
        /// Выполняет чтения файла списка терминов, сохраняя результат в коллекцию <seealso cref="readedWords"/>.
        /// </summary>
        /// <returns>Успех операции чтения.</returns>
        private bool ReadXml()
        {
            XmlDocument document = new XmlDocument();

            try
            {
                document.Load(filePath);

                foreach (XmlElement element in document.GetElementsByTagName("word"))
                {
                    int ID = 0;
                    string answer = String.Empty;
                    string question = String.Empty;

                    foreach (XmlElement subElement in element)
                    {
                        if (subElement.Name == "ID")
                        { ID = int.Parse(subElement.InnerText); }
                        else if (subElement.Name == "answer")
                        { answer = subElement.InnerText; }
                        else if (subElement.Name == "question")
                        { question = subElement.InnerText; }
                    }

                    readedWords.Add(new EditorWord(question, answer));
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
        /// Запускает цепочку вызовов для сохранения коллекции терминов по пути, который получает как результат отображения окна сохранения или через переданный параметр.
        /// </summary>
        /// <param name="path">Путь, по которому требуется сохранить коллекцию. Передать <see cref="String.Empty"/>, если требуется сохранить в новый файл.</param>
        /// <param name="input">Коллекция терминов.</param>
        /// <param name="success">Успех операции сохранения.</param>
        public void SaveFile(string path, ObservableCollection<EditorWord> input, out bool success)
        {
            filePath = path;
            inputWords = input;
            wordsCount = inputWords.Count;

            if (filePath == String.Empty)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.DefaultExt = "cwtf";
                saveFileDialog1.Filter = "Crossword Creator List File (*.cwtf)|*.cwtf|Все файлы|*.*";                
                saveFileDialog1.Title = "Сохранить файл списка определений";

                BlurHandler.Blur(true);
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    fileName = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);
                    filePath = saveFileDialog1.FileName;

                    CreateNewXml();
                    WriteToXmlFile();
                    success = true;
                }
                else
                { success = false; }
                BlurHandler.Blur(false);
            }
            else
            {
                CreateNewXml();
                WriteToXmlFile();
                fileName = Path.GetFileNameWithoutExtension(filePath);
                success = true;
            }            
        }

        /// <summary>
        /// Создает новый файл списка терминов.
        /// </summary>
        private void CreateNewXml()
        {
            XmlTextWriter xmlTextWritter =
            new XmlTextWriter(filePath, Encoding.UTF8);

            xmlTextWritter.WriteStartDocument();
            xmlTextWritter.WriteStartElement("head");
            xmlTextWritter.WriteEndElement();
            xmlTextWritter.Close();
        }

        /// <summary>
        /// Записывает коллекцию терминов в файл.
        /// </summary>
        private void WriteToXmlFile()
        {
            XmlDocument document = new XmlDocument();
            document.Load(filePath);

            for (int i = 0; i < wordsCount; i++)
            {
                XmlNode word = document.CreateElement("word");
                document.DocumentElement.AppendChild(word);

                XmlNode wordID = document.CreateElement("ID");
                wordID.InnerText = Convert.ToString(i);
                word.AppendChild(wordID);

                XmlNode answer = document.CreateElement("answer");
                answer.InnerText = inputWords[i].Word;
                word.AppendChild(answer);

                XmlNode question = document.CreateElement("question");
                question.InnerText = inputWords[i].Question;
                word.AppendChild(question);
            }

            document.Save(filePath);
        }

        /// <summary>
        /// Восстанавливает все параметры <see cref="XmlEditor"/> на параметры по умолчанию.
        /// </summary>
        public void Reset()
        {
            readedWords = new List<EditorWord>();
            inputWords = null;
            fileName = String.Empty;
            filePath = String.Empty;
            wordsCount = 0;
        }
    }
}
