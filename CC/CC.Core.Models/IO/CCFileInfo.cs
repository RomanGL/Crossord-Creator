using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.Core.Models.IO
{
    public sealed class CCFileInfo : IFile
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public CCFileType Type { get; set; }

        public CCVersion Version { get; set; }

        public bool IsReadOnly { get; set; }
    }
}
