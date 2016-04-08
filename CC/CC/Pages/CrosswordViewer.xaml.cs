using System;
using System.Collections.Generic;
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
using FirstFloor.ModernUI.Presentation;
using Crossword_Application_Modern.UserTemplates;

namespace Crossword_Application_Modern.Pages
{
    /// <summary>
    /// Interaction logic for CrosswordViewer.xaml
    /// </summary>
    public partial class CrosswordViewer : UserControl
    {
        private string gridFileName;
        private string gridFilePath;
        private WordThumb currentWord;
        private bool correctly = true;
        private bool incorrectWordsShowed = false;
        ObservableCollection<EditorWord> words = new ObservableCollection<EditorWord>();
        private List<int> notCorrentIndexes = new List<int>();

        public CrosswordViewer()
        {
            InitializeComponent();
            questionsListBox.ItemsSource = words;
            checkCrosswordButton.IsEnabled = false;
            TryOpenFileFromInitialPath();
            separator1.Visibility = System.Windows.Visibility.Collapsed;
            wordGrid.Visibility = System.Windows.Visibility.Collapsed;
        }

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
                        }

                        FillCollectionFromGridFile(gridXmlHandler1.GetReadedWords);
                        checkCrosswordButton.IsEnabled = true;
                    }
                    InitialFilePath.Handled();
                }
            }            
        }

        private void New()
        {
            gridFilePath = String.Empty;
            gridFileName = String.Empty;
            words.Clear();
            contentCanvas.Children.Clear();
            notCorrentIndexes.Clear();
            incorrectWordsShowed = false;
        }

        private void openCrosswordButton_Click(object sender, RoutedEventArgs e)
        { OpenGrid(); }

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
                }

                FillCollectionFromGridFile(gridXmlHandler1.GetReadedWords);
                checkCrosswordButton.IsEnabled = true;
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
            thumb1.GotFocus += thumb1_GotFocus;
        }

        void thumb1_GotFocus(object sender, RoutedEventArgs e)
        {
            wordGrid.Visibility = System.Windows.Visibility.Visible;
            separator1.Visibility = System.Windows.Visibility.Visible;
            HideCurrentWordShadow();
            if (incorrectWordsShowed == true)
            {
                HideIncorrectWordsShadow();
            }

            WordThumb thumb = e.Source as WordThumb;
            WordRectangle wr = (WordRectangle)thumb.Template.FindName("wordRect", thumb);
            questionsListBox.SelectedIndex = wr.ID;
        }

        /// <summary>
        /// Выделяет слово на поле и показывает поле ввода ответа.
        /// </summary>
        /// <param name="thumb">Слово, которое требуется выделить.</param>
        private void SelectWord(WordThumb thumb)
        {
            Canvas.SetZIndex(thumb, Canvas.GetZIndex(currentWord) + 1);
            WordRectangle wr = (WordRectangle)thumb.Template.FindName("wordRect", thumb);

            idTextBlock.Text = wr.ID.ToString();
            questionTextBlock.Text = wr.Question;
            answerTextBox.Text = wr.InputedWord;
            answerTextBox.MaxLength = wr.Length;
            answerTextBox.Focus();
            wr.StartDropShadowAnimation();

            StartWordGridUnHideAnimation();
        }

        /// <summary>
        /// Скрывает выделение текущего слова.
        /// </summary>
        private void HideCurrentWordShadow()
        {
            if (currentWord != null)
            {
                Canvas.SetZIndex(currentWord, Canvas.GetZIndex(currentWord) - 1);
                WordRectangle wr1 = (WordRectangle)currentWord.Template.FindName("wordRect", currentWord);
                wr1.StartUnDropShadowAnimation();
                StartWordGridHideAnimation();
                currentWord = null;
            }
        }

        /// <summary>
        /// Скрывает панель ввода ответа.
        /// </summary>
        private void HideWordGridPanel()
        {
            StartWordGridHideAnimation();
        }

        void thumb1_Loaded(object sender, RoutedEventArgs e)
        {
            var obj = e.Source as WordThumb;
            if (!obj.wasCreated)
            {
                WordRectangle wr = (WordRectangle)obj.Template.FindName("wordRect", obj);
                wr.CreateEmptyWordRectangles(obj.currentWord);
                wr.SetOrientation(obj.currentWord.Orientation);
                wr.canRotate = false;
                obj.wasCreated = true;
            }
        }

        private void FillCollectionFromGridFile(List<GridWord> inputWords)
        {
            foreach (var item in inputWords)
            { words.Add(new EditorWord(item.Question, item.Word, item.ID)); }
        }        

        private void answerTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (currentWord != null)
            {
                WordRectangle wr = (WordRectangle)currentWord.Template.FindName("wordRect", currentWord);
                wr.SetText(answerTextBox.Text.ToLower());
            }            
        }

        #region Animations

        private void StartWordGridHideAnimation()
        {
            Storyboard HideAnimation = (Storyboard)this.Resources["WordGridHideAnimation"];
            HideAnimation.Begin();
        }

        private void StartWordGridUnHideAnimation()
        {
            Storyboard UnHideAnimation = (Storyboard)this.Resources["WordGridUnHideAnimation"];
            UnHideAnimation.Begin();
        }
        #endregion 

        private void checkCrosswordButton_Click(object sender, RoutedEventArgs e)
        { CheckCrossword(); }

        /// <summary>
        /// Выполняет проверку кроссворда на правильность заполнения и выводит окно с результатом.
        /// </summary>
        private void CheckCrossword()
        {
            correctly = true;
            notCorrentIndexes.Clear();
            HideCurrentWordShadow();
            foreach (WordThumb item in contentCanvas.Children)
            {
                WordRectangle wr = (WordRectangle)item.Template.FindName("wordRect", item);
                if (wr.Check() == false)
                { 
                    correctly = false;
                    notCorrentIndexes.Add(wr.ID);
                }
            }

            if (correctly == true)
            {
                Color previousColor = AppearanceManager.Current.AccentColor;
                AppearanceManager.Current.AccentColor = Colors.Green;
                Messages.CrosswordFilledIsCorrectly();
                AppearanceManager.Current.AccentColor = previousColor;
            }
            else
            {
                Color previousColor = AppearanceManager.Current.AccentColor;
                AppearanceManager.Current.AccentColor = Colors.Red;
                MessageBoxResult _result = Messages.CrosswordFilledIsNotCorrectly();
                AppearanceManager.Current.AccentColor = previousColor;

                if (_result == MessageBoxResult.Yes)
                {
                    ShowIncorrectWordsShadow();
                }
            }
        }

        /// <summary>
        /// Выделяет неверно заполненные слова на поле.
        /// </summary>
        private void ShowIncorrectWordsShadow()
        {
            foreach (var id in notCorrentIndexes)
            {
                WordThumb obj = contentCanvas.Children[id] as WordThumb;
                WordRectangle wr = (WordRectangle)obj.Template.FindName("wordRect", obj);
                wr.StartDropShadowAnimation();
            }
            incorrectWordsShowed = true;
        }

        /// <summary>
        /// Убирает выделение неправильно заполненных слов на поле.
        /// </summary>
        private void HideIncorrectWordsShadow()
        {
            foreach (var id in notCorrentIndexes)
            {
                WordThumb obj = contentCanvas.Children[id] as WordThumb;
                WordRectangle wr = (WordRectangle)obj.Template.FindName("wordRect", obj);
                wr.StartUnDropShadowAnimation();
            }
            notCorrentIndexes.Clear();
            incorrectWordsShowed = false;
        }

        /// <summary>
        /// Выполняет поиск пересечений в словах.
        /// </summary>
        private void FindIntersection()
        {
            
        }

        private void questionsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (questionsListBox.SelectedIndex != -1)
            {
                if (incorrectWordsShowed == true)
                { HideIncorrectWordsShadow(); }

                currentWord = contentCanvas.Children[questionsListBox.SelectedIndex] as WordThumb;
                SelectWord(currentWord);
            }
        }

        private void answerTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            HideWordGridPanel();
            HideCurrentWordShadow();
            questionsListBox.SelectedIndex = -1;
        }
    }
}
