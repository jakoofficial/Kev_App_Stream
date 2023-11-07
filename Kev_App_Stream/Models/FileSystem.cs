using Avalonia.Controls;
using Kev_App_Stream.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using System.IO;
using Newtonsoft.Json;
using Kev_App_Stream.ViewModels;

namespace Kev_App_Stream.Models
{
    public static class FileSystem
    {
        public static FilePickerFileType jsonType { get; } = new("json files")
        {
            Patterns = new[] { "*.json"}, //Windows, Linux, Web
            AppleUniformTypeIdentifiers = new[] {"public.json"}, //Apple, IOS, macOS
            MimeTypes = new[] {"json/*"} //Web, Windows, IOS
        };
        public static async void SaveList<T>(string prePathIdentifier, List<T> list)
        {
            var tl = TopLevel.GetTopLevel(MainWindow.Instance.topLevel);

            var file = await tl.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
            {
                Title = $"Save {prePathIdentifier}",
                FileTypeChoices = new[] {jsonType}
            });

            if (file is not null)
            {
                JsonSerializer serializer = new JsonSerializer();
                await using var stream = await file.OpenWriteAsync();
                using (StreamWriter streamWriter = new StreamWriter(stream))
                using (JsonWriter writer = new JsonTextWriter(streamWriter))
                {
                    serializer.Serialize(writer, list);
                }
            }
        }

        public static async void LoadList(string prePathIdentifier, List<string> list)
        {
            var tl = TopLevel.GetTopLevel(MainWindow.Instance.topLevel);

            var file = await tl.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = $"Open {prePathIdentifier}",
                AllowMultiple = false,
                FileTypeFilter = new[] { jsonType}
            });

            if (file.Count >= 1)
            {
                JsonSerializer serializer = new JsonSerializer();
                await using var stream = await file[0].OpenReadAsync();
                using var streamReader = new StreamReader(stream);
                var fileContent = await streamReader.ReadToEndAsync();

                List<string> iListContent = JsonConvert.DeserializeObject<List<string>>(fileContent);

                list.Clear();
                foreach(var item in iListContent)
                {
                    list.Add(item);
                }
                SetContentArea.Navigate(new SetUpScreenViewModel());

            }
        }
    }
}
