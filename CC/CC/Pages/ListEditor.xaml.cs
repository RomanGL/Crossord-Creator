using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Crossword_Application_Modern.ViewModel;

namespace Crossword_Application_Modern.Pages
{
    /// <summary>
    /// Interaction logic for ListEditor.xaml
    /// </summary>
    public partial class ListEditor : UserControl
    {
        private ListEditorViewModel _viewModel = new ListEditorViewModel();        

        public ListEditor()
        {
            InitializeComponent();
            this.DataContext = _viewModel;
            _viewModel.TryOpenFileFromInitialPath();
        }

        #region Обработчики команд  
      
        /// <summary>
        /// Обработчик команды <see cref="ApplicationCommands.New"/>.
        /// </summary>
        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _viewModel.New(true);
        }

        /// <summary>
        /// Обработчик команды <see cref="ApplicationCommands.Save"/>.
        /// </summary>
        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _viewModel.Save();
        }

        /// <summary>
        /// Обработчик команды <see cref="ApplicationCommands.SaveAs"/>.
        /// </summary>
        private void SaveAs_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _viewModel.SaveAs();
        }

        /// <summary>
        /// Определяет, возможно ли выполнение команд <see cref="ApplicationCommands.Save"/>
        /// и <see cref="ApplicationCommands.SaveAs"/>.
        /// </summary>
        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _viewModel.IsDirty;
        }

        /// <summary>
        /// Обработчик комманды <see cref="CustomCommands.AddNewItem"/>.
        /// </summary>
        private void AddNewItem_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            List<string> result = e.Parameter as List<string>;
            _viewModel.AddNewItem(result[0], result[1]);            
        }

        /// <summary>
        /// Обработчик команды <see cref="ApplicationCommands.Open"/>.
        /// </summary>
        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _viewModel.Open();
        }

        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _viewModel.DeleteItem(contentListBox.SelectedIndex);
        }

        private void Delete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (contentListBox.SelectedIndex == -1)
            {
                e.CanExecute = false;
            }
            else
            { e.CanExecute = true; }
        }

        #endregion

        /// <summary>
        /// Обработчик события изменения текста в панели редактирования терминов.
        /// </summary>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.IsDirty = true;
        }
    }
}
