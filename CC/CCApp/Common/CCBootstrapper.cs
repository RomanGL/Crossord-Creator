using CC.Core.Services;
using CC.Core.Services.Interfaces;
using CCApp.Services;
using Microsoft.Practices.Unity;
using Prism.Mvvm;
using Prism.Unity;
using System;
using System.Globalization;
using System.Windows;

namespace CCApp.Common
{
    public sealed class CCBootstrapper : UnityBootstrapper
    {
        public Window MainWindow { get { return Shell as Window; } }

        protected override void ConfigureViewModelLocator()
        {
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(GetViewModelType);
            ViewModelLocationProvider.SetDefaultViewModelFactory(GetViewModel);
        }

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {            
            App.Current.MainWindow = MainWindow;           
            MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            Container.RegisterType<INavigationParametersResolver, NavigationParametersResolver>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ICryptographyService, CryptographyService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IFileService, FileService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IXmlListService, XmlListService>(new ContainerControlledLifetimeManager());            
            Container.RegisterType<IZipFileService, ZipFileService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IFileInfoService, FileInfoService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IBlurService, BlurService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IMessagesService, MessagesService>(new ContainerControlledLifetimeManager());

            base.ConfigureContainer();
        }

        private object GetViewModel(Type viewModelType)
        {
            return Container.Resolve(viewModelType, viewModelType.Name);
        }

        private Type GetViewModelType(Type viewType)
        {
            string viewModelTypeName = null;
            if (viewType.Name.EndsWith("View"))
                viewModelTypeName = String.Format(CultureInfo.InvariantCulture, VIEW_MODEL_FORMAT, viewType.Name);
            else
                viewModelTypeName = String.Format(CultureInfo.InvariantCulture, VIEW_MODEL_CONTROLS_FORMAT, viewType.Name);

            return Type.GetType(viewModelTypeName);
        }

        private const string VIEW_MODEL_FORMAT = "CC.Core.ViewModels.{0}Model, CC.Core.ViewModels, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null";
        private const string VIEW_MODEL_CONTROLS_FORMAT = "CC.Core.ViewModels.{0}ViewModel, CC.Core.ViewModels, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null";
    }
}
