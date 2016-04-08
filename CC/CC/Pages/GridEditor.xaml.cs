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
using System.Windows.Shapes;
using FirstFloor.ModernUI.Windows.Controls;
using Crossword_Application_Modern.ViewModel;

namespace Crossword_Application_Modern.Pages
{
    /// <summary>
    /// Редактор сетки (тестовый).
    /// </summary>
    public partial class GridEditor : UserControl
    {
        GridEditorViewModel _viewModel = new GridEditorViewModel();

        public GridEditor()
        {
            InitializeComponent();
            this.DataContext = _viewModel;
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
        /// Обработчик комманды <see cref="CustomCommands.AddNewItem"/>. Добавляет новый список.
        /// </summary>
        private void AddNewItem_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _viewModel.LoadListFile();
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
            Messages.ShowMessage("Команда", "Delete");
        }

        private void Delete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion
    }   
}
