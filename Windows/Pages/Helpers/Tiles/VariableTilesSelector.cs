using SharedLibrary.Models;
using SharedLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Pages.Helpers.Tiles
{
    public class VariableTilesSelector : DataTemplateSelector
    {
        public DataTemplate SingleRowItemTemplate { get; set; }
        public DataTemplate DoubleRowItemTemplate { get; set; }
        public DataTemplate TripleRowItemTemplate { get; set; }
        public DataTemplate QuadRowItemTemplate { get; set; }
        public DataTemplate FullColumnItemTemplate { get; set; }
        public DataTemplate AdItemTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            if (element != null && item != null)
            {
                if (item is SampleDataItemViewModel)
                {
                    return SingleRowItemTemplate;
                } 
                else if (item is SampleAdViewModel)
                {
                    return AdItemTemplate;
                }
            }
            return base.SelectTemplateCore(item, container);
        }
    }
}
