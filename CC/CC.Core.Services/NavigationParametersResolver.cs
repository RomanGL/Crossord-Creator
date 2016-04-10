using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CC.Core.Services.Interfaces;

namespace CC.Core.Services
{
    public class NavigationParametersResolver : INavigationParametersResolver
    {
        public Dictionary<string, string> GetNavigationParameters(Uri uri)
        {
            var uriParts = uri.ToString().Split('?');
            if (uriParts.Length == 2)
            {
                var result = new Dictionary<string, string>();
                foreach (string block in uriParts[1].Split('&'))
                {
                    string name = null;
                    string value = null;

                    if (TryGetParam(block, out name, out value))
                        result[name] = value;
                }

                return result;
            }

            return null;
        }

        private bool TryGetParam(string block, out string name, out string value)
        {
            var parts = block.Split('=');
            if (parts.Length == 2)
            {
                name = parts[0];
                value = parts[1];
                return true;
            }

            name = null;
            value = null;

            return false;
        }
    }
}
