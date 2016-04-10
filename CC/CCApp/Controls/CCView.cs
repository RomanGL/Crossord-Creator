using CC.Core.ViewModels;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using System;
using System.Windows.Controls;

namespace CCApp.Controls
{
    public abstract class CCView : UserControl, IContent
    {
        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
            CurrentViewModel?.OnNavigatedFrom(e);
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            CurrentViewModel?.OnNavigatedTo(e);
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            CurrentViewModel?.OnNavigatingFrom(e);
        }

        private IViewModel CurrentViewModel { get { return DataContext as IViewModel; } }
    }
}
