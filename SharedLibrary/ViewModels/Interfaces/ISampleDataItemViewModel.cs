using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.ViewModels.Interfaces
{
    public interface ISampleDataItemViewModel
    {
        int ColSpan { get; set; }
        int RowSpan { get; set; }
    }
}
