using System.Collections.Generic;

namespace Omu.Awem.Helpers
{
    internal class ColumnModTag
    {
        public bool Nohide { get; set; }
        
        // inline formats
        public IList<InlElm> Format { get; set; }

        // filter formats
        public IList<FilterElm> Filters { get; set; }

        public int? Autohide { get; set; }

        // inline format func
        public string FormatFunc { get; set; }

        public string Caption { get; set; }
    }
}