using SharedLibrary.Infrastructure;
using SharedLibrary.Models;
using SharedLibrary.Services.Interfaces;
using SharedLibrary.ViewModels;
using SharedLibrary.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;

namespace Pages.SampleData
{
    public sealed class SampleDataSource
    {
        private const int MAX_ROW_SPAN = 4;
        private static SampleDataSource _sampleDataSource = new SampleDataSource();

        private ObservableCollection<SampleDataGroup> _groups = new ObservableCollection<SampleDataGroup>();
        public ObservableCollection<SampleDataGroup> Groups
        {
            get { return this._groups; }
        }

        public static async Task<IEnumerable<SampleDataGroup>> GetGroupsAsync()
        {
            await _sampleDataSource.GetSampleDataAsync();

            return _sampleDataSource.Groups;
        }

        public static async Task<SampleDataGroup> GetGroupAsync(string uniqueId)
        {
            await _sampleDataSource.GetSampleDataAsync();
            // Simple linear search is acceptable for small data sets
            var matches = _sampleDataSource.Groups.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static async Task<ISampleDataItemViewModel> GetItemAsync(string uniqueId)
        {
            await _sampleDataSource.GetSampleDataAsync();
            // Simple linear search is acceptable for small data sets
            var matches = _sampleDataSource.Groups.SelectMany(group => group.Items).Where((item) =>
            {
                if (item is SampleDataItemViewModel)
                    return ((SampleDataItemViewModel)item).Item.UniqueId.Equals(uniqueId);
                return false;
            });
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        private async Task GetSampleDataAsync()
        {
            if (this._groups.Count != 0)
                return;

            int max_rows = ServiceLocator.Resolve<IDeviceSpecificsService>().MaxRows;
            Uri dataUri = new Uri("ms-appx:///Pages/SampleData/SampleData.json");

            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
            string jsonText = await FileIO.ReadTextAsync(file);
            JsonObject jsonObject = JsonObject.Parse(jsonText);
            JsonArray jsonArray = jsonObject["Groups"].GetArray();

            var random = new Random();
            foreach (JsonValue groupValue in jsonArray)
            {
                JsonObject groupObject = groupValue.GetObject();
                SampleDataGroup group = new SampleDataGroup(groupObject["UniqueId"].GetString(),
                                                            groupObject["Title"].GetString(),
                                                            groupObject["Subtitle"].GetString(),
                                                            groupObject["ImagePath"].GetString(),
                                                            groupObject["Description"].GetString());

                var arrayOfItems = groupObject["Items"].GetArray();
                var index = 0;
                var currentRowCount = 0;
                var numberOfItems = arrayOfItems.Count;
                
                foreach (JsonValue itemValue in arrayOfItems)
                {
                    int rowSpan = 1;
                    if (currentRowCount == 0)
                    {
                        Debug.WriteLine("New Column");
                        rowSpan = random.Next(4) + 1;
                    }
                    else
                    {
                        if (currentRowCount < max_rows-2)
                            rowSpan = Math.Min(MAX_ROW_SPAN,random.Next(max_rows-currentRowCount) + 1);
                        else
                            rowSpan = max_rows - currentRowCount;
                    }                    
                    currentRowCount += rowSpan;
                    currentRowCount %= max_rows;

                    Debug.WriteLine("{0}", rowSpan);
                    JsonObject itemObject = itemValue.GetObject();
                    group.Items.Add(new SampleDataItemViewModel(new SampleDataItem(itemObject["UniqueId"].GetString(),
                                                       itemObject["Title"].GetString(),
                                                       itemObject["Subtitle"].GetString(),
                                                       itemObject["ImagePath"].GetString(),
                                                       itemObject["Description"].GetString(),
                                                       itemObject["Content"].GetString()))
                                                       {
                                                           ColSpan = 1,
                                                           RowSpan = rowSpan,
                                                       });
                }
                Debug.WriteLine("New Group");
                if (currentRowCount > 0)
                    group.Items.Add(new SampleAdViewModel("Assets/White.png")
                    {
                        ColSpan = 1,
                        RowSpan = max_rows - currentRowCount,
                    });
                this.Groups.Add(group);
            }
        }
    }
}
