using SharedLibrary.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.ViewModels
{
    public class SampleAdViewModel: ISampleDataItemViewModel
    {
        public SampleAdViewModel(string imagePath)
        {
            ImagePath = imagePath;
        }

        public string ImagePath { get; set; }
        public int ColSpan { get; set; }
        public int RowSpan { get; set; }
    }
}
