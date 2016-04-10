using FirstFloor.ModernUI.Windows;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCApp.Common
{
    public class CCContentLoader : DefaultContentLoader
    {
        public CCContentLoader(IUnityContainer container)
        {
            _container = container;
        }

        protected override object LoadContent(Uri uri)
        {
            return null;
        }

        private readonly IUnityContainer _container;
    }
}
