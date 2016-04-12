using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstFloor.ModernUI.Windows.Navigation;
using CC.Core.Services.Interfaces;
using Microsoft.Practices.Unity;

namespace CC.Core.ViewModels
{
    public class ListEditorViewModel : ViewModelBase
    {
        public ListEditorViewModel(INavigationParametersResolver navigationParametersResolver)
        {
            _navigationParametersResolver = navigationParametersResolver;
        }

        [OptionalDependency("JsonListService")]
        public IListService JsonListService { get; set; }

        [OptionalDependency("XmlListService")]
        public IListService XmlListService { get; set; }

        public override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private readonly INavigationParametersResolver _navigationParametersResolver;
    }
}
