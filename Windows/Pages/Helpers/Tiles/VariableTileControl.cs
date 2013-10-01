using SharedLibrary.Models;
using SharedLibrary.ViewModels;
using SharedLibrary.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Pages.Helpers.Tiles
{
    public class VariableTileControl : GridView
    {
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            Rect windowBounds = Window.Current.Bounds;

            //change num visible items in height based on window height
            //element.SetValue(VariableSizedWrapGrid.MaximumRowsOrColumnsProperty, 4);

            if (item is ISampleDataItemViewModel)
            {
                var viewModel = item as ISampleDataItemViewModel;
                element.SetValue(VariableSizedWrapGrid.ColumnSpanProperty, viewModel.ColSpan);
                element.SetValue(VariableSizedWrapGrid.RowSpanProperty, viewModel.RowSpan);
                base.PrepareContainerForItemOverride(element, item);
            }
        }
    }
}
