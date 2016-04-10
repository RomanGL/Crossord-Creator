using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstFloor.ModernUI.Windows.Navigation;
using CC.Core.Services.Interfaces;

namespace CC.Core.ViewModels
{
    public class ListEditorViewModel : ViewModelBase
    {
        public ListEditorViewModel(INavigationParametersResolver navigationParametersResolver)
        {
            _navigationParametersResolver = navigationParametersResolver;
        }

        public override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private readonly INavigationParametersResolver _navigationParametersResolver;
    }
}
