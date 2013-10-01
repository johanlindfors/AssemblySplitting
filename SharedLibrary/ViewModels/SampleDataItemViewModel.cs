using SharedLibrary.Infrastructure;
using SharedLibrary.Models;
using SharedLibrary.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.ViewModels
{
    public class SampleDataItemViewModel : ObservableObject<SampleDataItem>, ISampleDataItemViewModel
    {
        public SampleDataItemViewModel(SampleDataItem item) : base(item)
        {

        }

        public int ColSpan { get; set; }
        public int RowSpan { get; set; }
    }
}
