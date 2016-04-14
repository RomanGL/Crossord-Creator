using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstFloor.ModernUI.Windows.Navigation;
using CC.Core.Services.Interfaces;
using Microsoft.Practices.Unity;
using Prism.Commands;
using PropertyChanged;
using FirstFloor.ModernUI.Windows.Controls;

namespace CC.Core.ViewModels
{
    [ImplementPropertyChanged]
    public class ListEditorViewModel : ViewModelBase
    {
        public ListEditorViewModel(INavigationParametersResolver navigationParametersResolver)
        {
            _navigationParametersResolver = navigationParametersResolver;

            SaveCommand = new DelegateCommand<bool?>(OnSaveCommand);
        }

        [DoNotNotify]
        [Dependency("JsonListService")]
        public IListService JsonListService { get; set; }

        [DoNotNotify]
        [Dependency("XmlListService")]
        public IListService XmlListService { get; set; }

        [DoNotNotify]
        public DelegateCommand<bool?> SaveCommand { get; private set; }

        public override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void OnSaveCommand(bool? isSaveAs)
        {
            ModernDialog.ShowMessage($"Save command invoked with parameter {isSaveAs}", "Crossword Creator", System.Windows.MessageBoxButton.OK);
        }

        private readonly INavigationParametersResolver _navigationParametersResolver;
    }
}
