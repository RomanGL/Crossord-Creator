using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using Crossword_Application_Modern.Commands;
using Crossword_Application_Modern.Model;
using Microsoft.Win32;
using FirstFloor.ModernUI.Windows.Controls;

namespace Crossword_Application_Modern.ViewModel
{
    /// <summary>
    /// Модель представления редактора списков терминов (<seealso cref="ListEditor"/>).
    /// </summary>
    public class ListEditorViewModel : BaseViewModel
    {
        /// <summary>
        /// Имя файла по умолчанию.
        /// </summary>
        private const string _defaultFileName = "Безымянный список";
        /// <summary>
        /// Путь к файлу по умолчанию.
        /// </summary>
        private const string _defaultFilePath = null;

        /// <summary>
        /// Коллекция элементов <seealso cref="ListWordViewModel"/>.
        /// </summary>
        public ObservableCollection<ListWordViewModel> Items { get; set; }

        /// <summary>
        /// Указывает на наличие изменений.
        /// </summary>
        private bool isDirty;
        /// <summary>
        /// Путь до текущего файла.
        /// </summary>
        private string _currentFilePath;
        /// <summary>
        /// Имя текущего файла.
        /// </summary>
        private string _currentFileName;

        public ListEditorViewModel()
        {
            Items = new ObservableCollection<ListWordViewModel>();            
            Items.CollectionChanged += Items_CollectionChanged;
            New(false); 
        }

        /// <summary>
        /// Попытка открыть файл по пути, который получен при запуске приложения.
        /// </summary>
        public void TryOpenFileFromInitialPath()
        {
            if (InitialFilePath.GetFileExtension == FileExtension.CrosswordListFile
                && InitialFilePath.GetFilePath != String.Empty)
            {
                bool openSuccess = true;
                ObservableCollection<ListWordViewModel> result = 
                    new ObservableCollection<ListWordViewModel>(
                    Handlers.ListWordXmlHandler.Open(InitialFilePath.GetFilePath, out openSuccess)
                    .Select(e => new ListWordViewModel(e)));

                if (openSuccess == true)
                {
                    New(false);

                    Items = result;

                    FilePath = Handlers.ListWordXmlHandler.FilePath;
                    FileName = System.IO.Path.GetFileNameWithoutExtension(
                        Handlers.ListWordXmlHandler.FileName);
                }

                InitialFilePath.Handled();
            }
        }

        /// <summary>
        /// Обновить идентификаторы всех элементов коллекции.
        /// </summary>
        private void UpdateElementsID()
        {
            for (int i = 1; i <= Items.Count; i++)
            {
                Items[i - 1].ID = i;
            }
        }

        /// <summary>
        /// Обработчик события изменения коллекции. Устанавливает значение наличия изменений в True.
        /// </summary>
        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            isDirty = true;
        }

        #region Основные команды редактора

        /// <summary>
        /// Создание нового файла. Значение всех свойств устанавливаются на исходные.
        /// </summary>
        public void New(bool needTrySaveChanges)
        {
            if (needTrySaveChanges == true)
            {
                SaveChangesResult result = TrySaveChanges();

                if (result == SaveChangesResult.Saved ||
                    result == SaveChangesResult.NotRequired)
                {
                    FilePath = _defaultFilePath;
                    FileName = _defaultFileName;
                    Items.Clear();
                    isDirty = false;
                } 
            }
            else
            {
                FilePath = _defaultFilePath;
                FileName = _defaultFileName;
                Items.Clear();
                isDirty = false;
            }
        }

        /// <summary>
        /// Выполняет открытие файла. В случае наличия изменений, предлагает сохранить их.
        /// </summary>
        public void Open()
        {
            bool success = false;
            ObservableCollection<ListWordViewModel> readed = null;
                
            string path = null;
            string name = _defaultFileName;

            SaveChangesResult result = TrySaveChanges();

            if (result == SaveChangesResult.Saved || result == SaveChangesResult.NotRequired)
            {
                BlurHandler.Blur(true);

                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.RestoreDirectory = true;
                openFileDialog.DefaultExt = "cwtf";
                openFileDialog.Filter = "Crossword Creator List File (*.cwtf)|*.cwtf";
                openFileDialog.Title = "Открыть файл списка определений";

                if (openFileDialog.ShowDialog() == true)
                {
                    path = openFileDialog.FileName;
                    name = System.IO.Path.GetFileNameWithoutExtension(path);

                    readed = new ObservableCollection<ListWordViewModel>(
                        Handlers.ListWordXmlHandler.Open(path, out success)
                        .Select(e => new ListWordViewModel(e)));

                    if (success == true)
                    {
                        New(false);
                        foreach (var item in readed)
                        { Items.Add(item); }

                        FilePath = path;
                        FileName = name;
                        isDirty = false;
                    }                    
                }

                BlurHandler.Blur(false);
            }            
        }

        /// <summary>
        /// Выполняет операцию сохранения коллекции <see cref="Items"/>.
        /// </summary>
        /// <returns>Успех операции сохранения.</returns>
        public bool Save()
        {
            string path = FilePath;
            string name = FileName;
            bool success = false;

            if (path == null)
            { success = SaveAs(); }
            else
            {
                success = Handlers.ListWordXmlHandler.Save(path,
                    Items.Select(e => e.GetModel).ToList()); 
            }
            
            isDirty = !success;
            return success;
        } 
       
        /// <summary>
        /// Выполняет сохранение в новый файл.
        /// </summary>
        /// <returns>Успех операции сохранения.</returns>
        public bool SaveAs()
        {
            string path = null;
            string name = null;
            bool success = false;

            BlurHandler.Blur(true);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.DefaultExt = "cwtf";
            saveFileDialog.Filter = "Список терминов Crossword Creator (*.cwtf)|*.cwtf";
            saveFileDialog.Title = "Сохранить файл списка определений";

            if (saveFileDialog.ShowDialog() == true)
            {
                path = saveFileDialog.FileName;
                name = System.IO.Path.GetFileNameWithoutExtension(path);
                success = Handlers.ListWordXmlHandler.Save(path, 
                    Items.Select(e => e.GetModel).ToList());
            }

            if (success)
            {
                FilePath = path;
                FileName = name;
            }

            isDirty = !success;

            BlurHandler.Blur(false);
            return success;
        }

        /// <summary>
        /// Добавляет в коллекцию новый элемент.
        /// </summary>
        /// <param name="question">Вопрос.</param>
        /// <param name="answer">Ответ.</param>
        public void AddNewItem(string question, string answer)
        {
            if (String.IsNullOrWhiteSpace(question) || String.IsNullOrWhiteSpace(answer))
            { Messages.EmptyElement(); }
            else
            {
                Items.Insert(0, new ListWordViewModel()
                {
                    Question = question,
                    Answer = answer,
                    ID = 1
                });
                //UpdateElementsID();
                //Items.Add(new ListWordViewModel()
                //{
                //    Question = question,
                //    Answer = answer,
                //    ID = Count + 1
                //});
                Task update = new Task(UpdateElementsID);
                update.Start();

                isDirty = true;
            }
        }

        /// <summary>
        /// Удаляет элемент из коллекции.
        /// </summary>
        /// <param name="elementID">Индекс элемента в коллекции.</param>
        public void DeleteItem(int index)
        {
            Items.RemoveAt(index);
            Task task = new Task(UpdateElementsID);
            task.Start();
        }

        /// <summary>
        /// Выполняет попытку сохранения изменений.
        /// </summary>
        /// <returns>Результат сохранения.</returns>
        private SaveChangesResult TrySaveChanges()
        {
            if (isDirty == true)
            {
                var result = Messages.SaveTheChanges(FileName);
                switch (result)
                {
                    case MessageBoxResult.Cancel:
                        return SaveChangesResult.Cancel;

                    case MessageBoxResult.No:
                        return SaveChangesResult.NotRequired;

                    case MessageBoxResult.Yes:
                        if (Save() == true)
                        { return SaveChangesResult.Saved; }
                        else
                        { return SaveChangesResult.DontSaved; }
                    default:
                        return SaveChangesResult.Cancel;
                }
            }
            else
            { return SaveChangesResult.Saved; }
        }

        #endregion

        /// <summary>
        /// Возвращает количество элементов в коллекции <seealso cref="Items"/>.
        /// </summary>
        public int Count
        {
            get { return Items.Count; }
        }

        /// <summary>
        /// Возвращает значение, указывающее текущее имя файла.
        /// </summary>
        public string FileName
        {
            get { return _currentFileName; }
            private set
            {
                if (value != _currentFileName)
                {
                    _currentFileName = value;
                    OnPropertyChanged("FileName");
                }
            }
        }

        /// <summary>
        /// Возвращает значение, указывающее текущий путь до файла.
        /// </summary>
        public string FilePath
        {
            get { return _currentFilePath; }
            private set
            {
                if (value != _currentFilePath)
                {
                    _currentFilePath = value;
                    OnPropertyChanged("FilePath");
                }
            }
        }

        /// <summary>
        /// Возвращает или задает значение, указывающее на наличие изменений.
        /// </summary>
        public bool IsDirty
        {
            get { return isDirty; }
            set
            {
                if (value != isDirty)
                { isDirty = value; }
            }
        }
    }
}
