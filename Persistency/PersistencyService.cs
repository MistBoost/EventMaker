using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using EventMaker.Model;
using Newtonsoft.Json;

namespace EventMaker.Persistency
{
    public class PersistencyService
    {
        public static void SaveEventsAsJsonAsync(ObservableCollection<Event> events)
        {
            var json = JsonConvert.SerializeObject(events);
            SerializeEventsFileAsync(json, "events.json");
        }

        public static async Task<ObservableCollection<Event>> LoadEventsFromJsonAsync()
        {
            var json = await DeSerializeEventsFileAsync("events.json");
            return JsonConvert.DeserializeObject<ObservableCollection<Event>>(json);
        }

        public static async void SerializeEventsFileAsync(string eventsString, string fileName)
        {
            var folder = ApplicationData.Current.RoamingFolder;
            var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            using (var stream = await file.OpenStreamForWriteAsync())
            using (var writer = new StreamWriter(stream, Encoding.UTF8))
            {

                await writer.WriteAsync(eventsString);
            }
        }

        public static async Task<string> DeSerializeEventsFileAsync(string fileName)
        {
            var json = "";
            try
            {
                var folder = ApplicationData.Current.RoamingFolder;
                var file = await folder.GetFileAsync(fileName);
                using (var stream = await file.OpenStreamForReadAsync())
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    json = await reader.ReadToEndAsync();
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to deserialize json file");
            }
            return json;
        }
    }
}
