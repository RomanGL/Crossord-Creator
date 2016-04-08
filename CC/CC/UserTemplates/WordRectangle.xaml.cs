using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FirstFloor.ModernUI.Presentation;

namespace Crossword_Application_Modern.UserTemplates
{
    /// <summary>
    /// Логика взаимодействия для wordRectangle.xaml
    /// </summary>
    public partial class WordRectangle : UserControl
    {
        private GridWord gridWord = new GridWord();
        public bool canRotate { get; set; }
        private string inputedWord = String.Empty;
        Rect rectangle;
        Point leftTopPos;
        Size rectangleSize;

        public WordRectangle()
        {
            InitializeComponent();            
            stackPanel1.Orientation = gridWord.Orientation;
            canRotate = true;
        }

        public void CreateWordRectangles(GridWord _gridWord)
        {
            gridWord = _gridWord;            
            stackPanel1.Children.Clear();           
            
            TextBlock tb = new TextBlock();
            tb.Text = String.Format("{0}", gridWord.GridID.ToString());
            tb.TextAlignment = TextAlignment.Center;
            tb.FontSize = 19;
            tb.Width = 25;
            tb.Height = 25;
            tb.Style = FindResource("idTextBlock") as Style;
            stackPanel1.Children.Add(tb);

            foreach (char symbol in gridWord.Word)
            { stackPanel1.Children.Add(new Cell(symbol)); }

            CalculateRectangleSize();
            CalculateRectanglePosition();
            ResetRectangle();
        }

        public void CreateEmptyWordRectangles(GridWord _gridWord)
        {
            gridWord = _gridWord;
            stackPanel1.Children.Clear();

            TextBlock tb = new TextBlock();
            tb.Text = String.Format("{0}", gridWord.GridID.ToString());
            tb.TextAlignment = TextAlignment.Center;
            tb.FontSize = 19;
            tb.Width = 25;
            tb.Height = 25;
            tb.Style = FindResource("idTextBlock") as Style;
            stackPanel1.Children.Add(tb);

            for (int i = 0; i < gridWord.Word.Length; i++)
            { stackPanel1.Children.Add(new Cell()); }

            CalculateRectangleSize();
            CalculateRectanglePosition();
            ResetRectangle();            
        }

        /// <summary>
        /// Изменяет ориентацию контейнера на противоположную.
        /// </summary>
        private void Rotate()
        {
            switch (stackPanel1.Orientation)
            {
                case Orientation.Horizontal:
                    stackPanel1.Orientation = Orientation.Vertical;
                    break;
                case Orientation.Vertical:
                    stackPanel1.Orientation = Orientation.Horizontal;
                    break;
            }

            gridWord.Orientation = stackPanel1.Orientation;
            CalculateRectangleSize();
            ResetRectangle();
        }

        public void SelectIntersection(Rect intersetRect)
        {
            double rectLengthX = rectangle.X + rectangle.Width;
            double rectLengthY = rectangle.Y + rectangle.Height;
            double tempX = rectLengthX;
            double tempY = rectLengthY;
            double tempIntersectX = intersetRect.X;
            double tempIntersectY = intersetRect.Y;

            int countOfXOperations = 0;
            int countOfYOperations = 0;            
            int maxXOperations = 0;
            int maxYOperations = 0;

            if (Orientation == System.Windows.Controls.Orientation.Horizontal)
            {
                maxXOperations = stackPanel1.Children.Count - 1;
                maxYOperations = 1;
            }
            else if (Orientation == System.Windows.Controls.Orientation.Vertical)
            {
                maxXOperations = 1;
                maxYOperations = stackPanel1.Children.Count - 1;
            }

            List<double> cellsXPositions = new List<double>(maxXOperations + 1);
            List<double> cellsYPositions = new List<double>(maxYOperations + 1);
            cellsXPositions.Add(0);
            cellsYPositions.Add(0);

            while (countOfXOperations < maxXOperations)
            {
                tempX -= 24;  
                cellsXPositions.Add(tempX);
                countOfXOperations++;
            }

            while (countOfYOperations < maxYOperations)
            {
                tempY -= 24;
                cellsYPositions.Add(tempY);
                countOfYOperations++;
            }

            cellsXPositions.Reverse(1, cellsXPositions.Count - 1);
            cellsYPositions.Reverse(1, cellsYPositions.Count - 1);

            if (Orientation == System.Windows.Controls.Orientation.Horizontal)
            {
                for (int i = 1; i < maxXOperations + 1; i++)
                {
                    var obj = stackPanel1.Children[i] as Cell;

                    if (tempIntersectX == cellsXPositions[i])
                    {
                        obj.IsIntersected = true;
                        obj.SetBackgroundColor(AppearanceManager.Current.AccentColor);
                    }
                    else
                    { obj.SetDefaultBackroundBrush(); }

                    tempIntersectX--;
                }

                for (int i = 1; i < maxXOperations + 1; i++)
                {
                    var obj = stackPanel1.Children[i] as Cell;

                    if (obj.IsIntersected == false)
                    { obj.SetDefaultBackroundBrush(); }
                }
            }
            else if (Orientation == System.Windows.Controls.Orientation.Vertical)
            {
                for (int i = 1; i < maxYOperations + 1; i++)
                {
                    var obj = stackPanel1.Children[i] as Cell;

                    if (tempIntersectY == cellsYPositions[i])
                    {
                        obj.IsIntersected = true;
                        obj.SetBackgroundColor(AppearanceManager.Current.AccentColor);
                    }

                    tempIntersectY--;
                }

                for (int i = 1; i < maxYOperations + 1; i++)
                {
                    var obj = stackPanel1.Children[i] as Cell;

                    if (obj.IsIntersected == false)
                    { obj.SetDefaultBackroundBrush(); }
                }                
            }
        }

        /// <summary>
        /// Вычисляет прямоугольника контейнера.
        /// </summary>
        private void CalculateRectangleSize()
        {
            if (Orientation == System.Windows.Controls.Orientation.Vertical)
            { rectangleSize = new Size(24, (stackPanel1.Children.Count - 1) * 24); }
            else if (Orientation == System.Windows.Controls.Orientation.Horizontal)
            { rectangleSize = new Size((stackPanel1.Children.Count - 1) * 24, 24); }
        }

        /// <summary>
        /// Вычисляет позицию прямоугольника относительно верхнего левого угла.
        /// </summary>
        private void CalculateRectanglePosition()
        {
            if (Orientation == System.Windows.Controls.Orientation.Vertical)
            { leftTopPos = new Point(gridWord.X, gridWord.Y + 25); }
            else if (Orientation == System.Windows.Controls.Orientation.Horizontal)
            { leftTopPos = new Point(gridWord.X + 25, gridWord.Y); }
        }

        /// <summary>
        /// Переустанавливает параметры прямоугольника.
        /// </summary>
        private void ResetRectangle()
        { rectangle = new Rect(leftTopPos, rectangleSize); }

        /// <summary>
        /// Устанавливает ориентацию контейнера.
        /// </summary>
        /// <param name="_orientation">Требуемая ориентация.</param>
        public void SetOrientation(Orientation _orientation)
        {
            gridWord.Orientation = _orientation;
            stackPanel1.Orientation = gridWord.Orientation;
        }

        public void SetPosition(Point _point)
        {
            gridWord.X = _point.X;
            gridWord.Y = _point.Y;
            CalculateRectanglePosition();
            ResetRectangle();
        }

        public void SetText(string input)
        {
            inputedWord = input;
            for (int i = 1; i < Length + 1; i++)
            {
                Cell _cell = stackPanel1.Children[i] as Cell;

                if (i - 1 >= inputedWord.Length)
                { _cell.SetSymbol(' '); }
                else
                { _cell.SetSymbol(inputedWord[i - 1]); }
            }
        }

        public string InputedWord
        { get { return inputedWord; } }

        public int ID
        { get { return gridWord.ID; } }
        
        public int Length
        { get { return gridWord.Length(); } }

        public string Word
        { get { return gridWord.Word; } }

        public string Question
        { get { return gridWord.Question; } }

        public Orientation Orientation
        { get { return gridWord.Orientation; } }

        public Rect GetRect
        { get { return rectangle; } }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (canRotate == true)
            { Rotate(); }
        }

        public bool Check()
        {
            if (inputedWord == gridWord.Word)
            { return true; }
            else
            { return false; }
        }

        public void StartDropShadowAnimation()
        {
            Storyboard DropShadowAnimation = 
                (Storyboard)this.Resources["DropShadowAnimation"];
            DropShadowAnimation.Begin();
        }

        public void StartUnDropShadowAnimation()
        {
            Storyboard UnDropShadowAnimation =
                (Storyboard)this.Resources["UnDropShadowAnimation"];
            UnDropShadowAnimation.Begin();
        }
    }
}
