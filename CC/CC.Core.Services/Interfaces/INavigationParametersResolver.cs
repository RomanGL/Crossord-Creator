using System;
using System.Collections.Generic;

namespace CC.Core.Services.Interfaces
{
    public interface INavigationParametersResolver
    {
        Dictionary<string, string> GetNavigationParameters(Uri uri);
    }
}
