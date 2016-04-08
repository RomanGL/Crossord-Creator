using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using FirstFloor.ModernUI.Windows.Controls;
using System.IO;
using Crossword_Application_Modern.UserTemplates;

namespace Crossword_Application_Modern.Pages
{
    /// <summary>
    /// Interaction logic for CrosswordEditor.xaml
    /// </summary>
    public partial class CrosswordEditor : UserControl, INotifyPropertyChanged
    {
        private string questionsAndAnswersFileName;
        private string questionsAndAnswersFilePath;        
        private string gridFileName;
        private string gridFilePath;
        private int currentWordID = 0;
        private int countWordsInCanvas = -1;
        private int index;
        private List<int> indexesInContentCanvas = new List<int>();
        private bool changed = false;
        private bool isNew = true;
        private Brush previousCanvasBackgroundBrush;
        ObservableCollection<EditorWord> words = new ObservableCollection<EditorWord>();
        
        public CrosswordEditor()
        {
            InitializeComponent();
            wordsListBox.ItemsSource = words;
            New();
            TryOpenFileFromInitialPath();
        }     
   
        /// <summary>
        /// Попытка открыть файл по пути, полученного при инициалзации приложения.
        /// </summary>
        private void TryOpenFileFromInitialPath()
        {
            if (InitialFilePath.GetFileExtension == FileExtension.CrosswordGridFile)
            {
                if (InitialFilePath.GetFilePath != String.Empty)
                {
                    GridXmlHandler gridXmlHandler1 = new GridXmlHandler();
                    if (gridXmlHandler1.OpenFileFromPath(InitialFilePath.GetFilePath) == true)
                    {
                        New();
                        gridFilePath = gridXmlHandler1.GetFilePath;
                        gridFileName = gridXmlHandler1.GetFileName;

                        foreach (GridWord item in gridXmlHandler1.GetReadedWords)
                        {
                            AddWordToCanvas(item);
                            countWordsInCanvas++;
                        }

                        FillCollectionFromGridFile(gridXmlHandler1.GetReadedWords);
                    }
                    InitialFilePath.Handled();
                }                
            }            
        }

        /// <summary>
        /// Реинициализация всех параметров.
        /// </summary>
        private void New()
        {
            questionsAndAnswersFilePath = String.Empty;
            gridFilePath = String.Empty;
            questionsAndAnswersFileName = String.Empty;
            gridFileName = "Безымянный";
            currentWordID = 0;
            countWordsInCanvas = -1;
            words.Clear();
            contentCanvas.Children.Clear();
            changed = false;
        }

        private void thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var obj = e.Source as WordThumb;
            WordRectangle wr = (WordRectangle)obj.Template.FindName("wordRect", obj);
            double _x = Canvas.GetLeft(obj);
            double _y = Canvas.GetTop(obj);
            bool positionChanged = false;
            Canvas.SetZIndex(obj, 1);

            if (e.HorizontalChange >= 25)
            { 
                Canvas.SetLeft(obj, _x + 25);
                positionChanged = true;
            }
            else if (e.HorizontalChange <= -25)
            { 
                Canvas.SetLeft(obj, _x - 25);
                positionChanged = true;
            }

            if (e.VerticalChange >= 25)
            { 
                Canvas.SetTop(obj, _y + 25);
                positionChanged = true;
            }
            else if (e.VerticalChange <= -25)
            { 
                Canvas.SetTop(obj, _y - 25);                
                positionChanged = true;
            }

            //if (positionChanged == true)
            //{ 
            //    wr.SetPosition(new Point(Canvas.GetLeft(obj), Canvas.GetTop(obj)));
            //    FindIntersection(obj);
            //}            

            e.Handled = true;
            changed = true;
        }

        private void thumb1_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            var obj = e.Source as WordThumb;
            Canvas.SetZIndex(obj, 0);
        }

        private void openQA_Button_Click(object sender, RoutedEventArgs e)
        { 
            OpenQuestionsAndAnswersFile();            
        }

        /// <summary>
        /// Показывает откно выбора файла терминов. В случае успеха открывает выбранный файл.
        /// </summary>
        private void OpenQuestionsAndAnswersFile()
        {
            bool success;
            XmlHandler xmlHandler1 = new XmlHandler();
            xmlHandler1.OpenFile(out success);
            if (success)
            {
                if (isNew == false)
                {
                    MessageBoxResult result = Messages.SelectOperationWithLoadedListFile();

                    if (result == MessageBoxResult.Yes)
                    {
                        if (changed == true)
                        {
                            MessageBoxResult saveMessageResult =
                                Messages.SaveTheChanges(gridFileName);

                            if (saveMessageResult == MessageBoxResult.Yes)
                            {
                                if (SaveChanges(gridFilePath) == true)
                                {
                                    New();
                                    questionsAndAnswersFileName = xmlHandler1.fileName;
                                    questionsAndAnswersFilePath = xmlHandler1.filePath;
                                    FillCollectionFromListFile(xmlHandler1.readedWords);

                                    changed = false;
                                }
                            }
                            else if (saveMessageResult == MessageBoxResult.No)
                            {
                                New();
                                questionsAndAnswersFileName = xmlHandler1.fileName;
                                questionsAndAnswersFilePath = xmlHandler1.filePath;
                                FillCollectionFromListFile(xmlHandler1.readedWords);

                                changed = false;
                            }
                        }
                        else
                        {
                            New();
                            questionsAndAnswersFileName = xmlHandler1.fileName;
                            questionsAndAnswersFilePath = xmlHandler1.filePath;
                            FillCollectionFromListFile(xmlHandler1.readedWords);

                            changed = false;
                        }
                    }
                    else if (result == MessageBoxResult.No)
                    {
                        FillCollectionFromListFile(xmlHandler1.readedWords);
                    }
                }
                else
                {
                    New();
                    questionsAndAnswersFileName = xmlHandler1.fileName;
                    questionsAndAnswersFilePath = xmlHandler1.filePath;
                    FillCollectionFromListFile(xmlHandler1.readedWords);

                    changed = false;
                    isNew = false;
                }                
            }
        }

        /// <summary>
        /// Добавляет элементы коллекции в коллекцию списка слов.
        /// </summary>
        /// <param name="inputWords">Коллекция слов.</param>
        private void FillCollectionFromListFile(List<EditorWord> inputWords)
        {
            foreach (var item in inputWords)
            { words.Add(item); }
        }

        /// <summary>
        /// Добавляет элементы коллекции
        /// </summary>
        /// <param name="inputWords"></param>
        private void FillCollectionFromGridFile(List<GridWord> inputWords)
        {
            foreach (var item in inputWords)
            { words.Add(new EditorWord(item.Question, item.Word)); }
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        { changed = true; }
        
        private void TextBlock_MouseMove(object sender, MouseEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (textBlock != null && e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(textBlock,
                                     index.ToString(),
                                     DragDropEffects.Move);
            }
        }

        private void wordsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { index = wordsListBox.SelectedIndex; }

        private void contentCanvas_DragEnter(object sender, DragEventArgs e)
        {
            previousCanvasBackgroundBrush = contentCanvas.Background;

            string tempColor = FindResource("ItemBackgroundSelected").ToString();
            BrushConverter converter = new BrushConverter();
            Brush newBackground = (Brush)converter.ConvertFromString(tempColor);
            newBackground.Opacity = .3;

            contentCanvas.Background = newBackground;
        }

        private void contentCanvas_DragLeave(object sender, DragEventArgs e)
        { contentCanvas.Background = previousCanvasBackgroundBrush; }

        private void contentCanvas_Drop(object sender, DragEventArgs e)
        {
            contentCanvas.Background = previousCanvasBackgroundBrush;
            int parsedIndex;            

            string temp = (string)e.Data.GetData(DataFormats.Text);

            if (temp == null)
            { Messages.NotSupportedDroppedElementInCanvas(); }
            else
            {
                try
                {
                    parsedIndex = int.Parse(temp);
                    indexesInContentCanvas.Add(parsedIndex);
                    currentWordID = parsedIndex;
                    countWordsInCanvas++;
                    Point pos = e.GetPosition(contentCanvas);

                    double _x = (int)pos.X;
                    double _y = (int)pos.Y;

                    while (_x % 25 != 0)
                    {
                        _x++;
                    }

                    while (_y % 25 != 0)
                    {
                        _y++;
                    }

                    GridWord _gridWord = new GridWord(words[parsedIndex].Word, 
                        words[parsedIndex].Question, countWordsInCanvas, 
                        _x, _y, Orientation.Vertical);

                    AddWordToCanvas(_gridWord);
                    changed = true;
                }
                catch (FormatException)
                {
                    e.Effects = DragDropEffects.None;
                    Messages.NotSupportedDroppedElementInCanvas();
                }    
            }
        }

        private void AddWordToCanvas(GridWord _word)
        {
            WordThumb thumb1 = new WordThumb();
            thumb1.currentWord = _word;
            thumb1.Template = FindResource("RectangleTemplate") as ControlTemplate;
            contentCanvas.Children.Add(thumb1);
            Canvas.SetLeft(thumb1, _word.X);
            Canvas.SetTop(thumb1, _word.Y);            
            thumb1.Focusable = true;
            thumb1.ToolTip = thumb1.currentWord.Question;
            thumb1.Loaded += thumb1_Loaded;
            thumb1.DragDelta += thumb_DragDelta;
            thumb1.DragCompleted += thumb1_DragCompleted;
            thumb1.GotFocus += thumb1_GotFocus;
            thumb1.LostFocus += thumb1_LostFocus;
        }        

        void thumb1_LostFocus(object sender, RoutedEventArgs e)
        {
            var obj = e.Source as WordThumb;
            WordRectangle wr = (WordRectangle)obj.Template.FindName("wordRect", obj);

            wr.StartUnDropShadowAnimation();
        }

        void thumb1_GotFocus(object sender, RoutedEventArgs e)
        {
            var obj = e.Source as WordThumb;
            WordRectangle wr = (WordRectangle)obj.Template.FindName("wordRect", obj);

            wr.StartDropShadowAnimation();
        }

        void thumb1_Loaded(object sender, RoutedEventArgs e)
        {
            var obj = e.Source as WordThumb;
            if (!obj.wasCreated)
            {
                WordRectangle wr = (WordRectangle)obj.Template.FindName("wordRect", obj);
                wr.CreateWordRectangles(obj.currentWord);
                wr.SetOrientation(obj.currentWord.Orientation);
                obj.wasCreated = true;
            }            
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {                
                foreach (WordThumb item in contentCanvas.Children)
                {
                    if (item.IsFocused)
                    {
                        contentCanvas.Children.Remove(item);
                        changed = true;
                        break;
                    }
                }
            }
        }
      
        private void FindIntersection(WordThumb _word)
        {
            WordRectangle wordRect = (WordRectangle)_word.Template.FindName("wordRect", _word);

            foreach (var item in contentCanvas.Children)
            {
                var obj = item as WordThumb;
                if (obj.Equals(_word) == false)
                {
                    WordRectangle wr = (WordRectangle)obj.Template.FindName("wordRect", obj);
                    if (wordRect.GetRect.IntersectsWith(wr.GetRect) == true)
                    {
                        Rect resultIntersect = Rect.Intersect(wordRect.GetRect, wr.GetRect);
                        wordRect.SelectIntersection(resultIntersect);
                        wr.SelectIntersection(resultIntersect);
                    }                    
                }                
            }
        }

        private void openGridButton_Click(object sender, RoutedEventArgs e)
        {
            if (changed == true)
            {
                MessageBoxResult result = Messages.SaveTheChanges(gridFileName);
                if (result == MessageBoxResult.Yes)
                {
                    if (SaveChanges(gridFilePath) == true)
                    { OpenGrid(); }
                }
                else if (result == MessageBoxResult.No)
                { OpenGrid(); }
            }
            else
            { OpenGrid(); }
        }

        private void OpenGrid()
        {
            GridXmlHandler gridXmlHandler1 = new GridXmlHandler();
            if (gridXmlHandler1.OpenFileFromDialog() == true)
            {
                New();
                gridFilePath = gridXmlHandler1.GetFilePath;
                gridFileName = gridXmlHandler1.GetFileName;

                foreach (GridWord item in gridXmlHandler1.GetReadedWords)
                {
                    AddWordToCanvas(item);
                    countWordsInCanvas++;
                }

                FillCollectionFromGridFile(gridXmlHandler1.GetReadedWords);
                isNew = false;
            }
        }

        private void saveAsButton_Click(object sender, RoutedEventArgs e)
        { SaveChanges(String.Empty); }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        { SaveChanges(gridFilePath); }

        private bool SaveChanges(string path)
        {
            GridXmlHandler gridXmlHandler1 = new GridXmlHandler();
            gridXmlHandler1.SetInputWords(GetListOfGridWords());

            if (gridXmlHandler1.SaveFile(path) == true)
            {
                gridFilePath = gridXmlHandler1.GetFilePath;
                gridFileName = gridXmlHandler1.GetFileName;
                changed = false;
                return true;
            }

            return false;
        }

        private List<GridWord> GetListOfGridWords()
        {
            List<GridWord> gridWords = new List<GridWord>();

            for (int i = 0; i < contentCanvas.Children.Count; i++)
            {
                WordThumb item = contentCanvas.Children[i] as WordThumb;
                WordRectangle wr = (WordRectangle)item.Template.FindName("wordRect", item);

                string _word = wr.Word;
                string _question = wr.Question;
                int _ID = i;
                double _x = Canvas.GetLeft(item);
                double _y = Canvas.GetTop(item);
                Orientation _orientation = wr.Orientation;

                gridWords.Add(new GridWord(_word, _question, _ID, 
                    _x, _y, _orientation));
            }

            return gridWords;
        }

        private void SaveToPicture()
        {
            MessageBox.Show(String.Format("Ширина: {0}, Высота: {1}.", 
                crosswordGrid.RenderSize.Width, crosswordGrid.RenderSize.Height),
                "crossword grid");
            MessageBox.Show(String.Format("Ширина: {0}, Высота: {1}.",
                scrool.RenderSize.Width, scrool.RenderSize.Height),
                "scrool");
            MessageBox.Show(String.Format("Ширина: {0}, Высота: {1}.",
                contentCanvas.RenderSize.Width, contentCanvas.RenderSize.Height),
                "content canvas");

            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(800, 800, 96, 96, PixelFormats.Default);
            renderBitmap.Render(contentCanvas);

            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            using (FileStream imageFile = new FileStream("1.png", FileMode.Create, FileAccess.Write))
            {
                encoder.Save(imageFile);
                imageFile.Flush();
                imageFile.Close();
            }
        }

        private void saveAsPicture_Click(object sender, RoutedEventArgs e)
        {
            SaveToPicture();
        }

        double _width, _height;
        public double GridWidth
        {
            get { return _width; }
            set
            {
                if (value != _width)
                {
                    _width = value;
                    OnPropertyChanged("Width");
                }
            }
        }

        public double GridHeight
        {
            get { return _height; }
            set
            {
                if (value != _height)
                {
                    _height = value;
                    OnPropertyChanged("Height");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        { PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); }
    }
}
