using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
    /// Модель представления редактора сетки <seealso cref="GridEditor"/>.
    /// </summary>
    public class GridEditorViewModel : BaseViewModel
    {
        /// <summary>
        /// Имя файла по умолчанию.
        /// </summary>
        private const string _defaultFileName = "Безымянный кроссворд";
        /// <summary>
        /// Путь к файлу по умолчанию.
        /// </summary>
        private const string _defaultFilePath = null;

        /// <summary>
        /// Коллекция элементов <seealso cref="GridWordViewModel"/>.
        /// </summary>
        public ObservableCollection<GridWordViewModel> Items { get; set; }

        /// <summary>
        /// Коллекция элементов <seealso cref="ListWordViewModel"/>.
        /// </summary>
        public ObservableCollection<ListWordViewModel> WordList { get; set; }

        /// <summary>
        /// Указывает на наличие изменений.
        /// </summary>
        private bool isDirty;
        /// <summary>
        /// Указывает на наличие элементов в коллекции <see cref="Items"/>.
        /// </summary>
        private bool hasItems = false;
        /// <summary>
        /// Путь до текущего файла.
        /// </summary>
        private string _currentFilePath;
        /// <summary>
        /// Имя текущего файла.
        /// </summary>
        private string _currentFileName;

        public GridEditorViewModel()
        {
            Items = new ObservableCollection<GridWordViewModel>();
            Items.CollectionChanged += Items_CollectionChanged;
            WordList = new ObservableCollection<ListWordViewModel>();
            New(false);
        }

        /// <summary>
        /// Попытка открыть файл по пути, который получен при запуске приложения.
        /// </summary>
        public void TryOpenFileFromInitialPath()
        {
            if (InitialFilePath.GetFileExtension == FileExtension.CrosswordGridFile
                && InitialFilePath.GetFilePath != String.Empty)
            {
                bool openSuccess = true;
                ObservableCollection<GridWordViewModel> result =
                    new ObservableCollection<GridWordViewModel>(
                    Handlers.GridWordXmlHandler.Open(InitialFilePath.GetFilePath, out openSuccess)
                    .Select(e => new GridWordViewModel(e)));

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
        /// Обработчик события изменения коллекции. Устанавливает значение наличия изменений в True.
        /// </summary>
        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (Items.Count > 0)
            { HasItems = true; }
            else
            { HasItems = false; }

            isDirty = true;            
        }

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
            ObservableCollection<GridWordViewModel> readed = null;

            string path = null;
            string name = _defaultFileName;

            SaveChangesResult result = TrySaveChanges();

            if (result == SaveChangesResult.Saved || result == SaveChangesResult.NotRequired)
            {
                BlurHandler.Blur(true);

                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.RestoreDirectory = true;
                openFileDialog.DefaultExt = "cwgf";
                openFileDialog.Filter = "Crossword Creator Grid File (*.cwgf)|*.cwgf";
                openFileDialog.Title = "Открыть файл сетки кроссворда";

                if (openFileDialog.ShowDialog() == true)
                {
                    path = openFileDialog.FileName;
                    name = System.IO.Path.GetFileNameWithoutExtension(path);
                    readed = new ObservableCollection<GridWordViewModel>(
                        Handlers.GridWordXmlHandler.Open(path, out success)
                        .Select(e => new GridWordViewModel(e)));
                }                

                if (success == true)
                {
                    New(false);
                    foreach (var item in readed)
                    { Items.Add(item); }

                    FilePath = path;
                    FileName = name;
                    isDirty = false;
                }

                BlurHandler.Blur(false);
            }
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
                success = Handlers.GridWordXmlHandler.Save(path,
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
            saveFileDialog.DefaultExt = "cwgf";
            saveFileDialog.Filter = "Crossword Creator Grid File (*.cwgf)|*.cwgf";
            saveFileDialog.Title = "Сохранить файл сетки кроссворда";

            if (saveFileDialog.ShowDialog() == true)
            {
                path = saveFileDialog.FileName;
                name = System.IO.Path.GetFileNameWithoutExtension(path);
                success = Handlers.GridWordXmlHandler.Save(path,
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
        /// Выполняет загрузку файла списка терминов в список слов редактора сетки.
        /// </summary>
        /// <returns>Успех операции загрузки.</returns>
        public bool LoadListFile()
        {
            bool success = false;

            BlurHandler.Blur(true);

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.RestoreDirectory = true;
            openFileDialog.DefaultExt = "cwtf";
            openFileDialog.Filter = "Crossword Creator List File (*.cwtf)|*.cwtf";
            openFileDialog.Title = "Загрузить файл списка определений";

            if (openFileDialog.ShowDialog() == true)
            {
                string path = openFileDialog.FileName;
                List<ListWordModel> result = Handlers.ListWordXmlHandler.Open(path, out success);

                foreach (ListWordModel item in result)
                { WordList.Add(new ListWordViewModel(item)); }
            }

            BlurHandler.Blur(false);

            return success;
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

        /// <summary>
        /// Возвращает или задает значение, указывающее на наличие 
        /// объектов в коллекции <see cref="Items"/>.
        /// </summary>
        public bool HasItems
        {
            get { return hasItems; }
            set
            {
                if (value != hasItems)
                { 
                    hasItems = value;
                    OnPropertyChanged("HasItems");
                }
            }
        }
    }
}
