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
        public static void SaveGenericAsJsonAsync<T>(ObservableCollection<T> objects, string filename)
        {
            var json = JsonConvert.SerializeObject(objects);
            SerializeGenericFileAsync(json, filename);
        }

        public static async Task<ObservableCollection<T>> LoadGenericFromJsonAsync<T>(string filename)
        {
            var json = await DeSerializeGenericFileAsync(filename);
            return JsonConvert.DeserializeObject<ObservableCollection<T>>(json);
        }

        public static async void SerializeGenericFileAsync(string jsonstring, string fileName)
        {
            var folder = ApplicationData.Current.RoamingFolder;
            var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            using (var stream = await file.OpenStreamForWriteAsync())
            using (var writer = new StreamWriter(stream, Encoding.UTF8))
            {

                await writer.WriteAsync(jsonstring);
            }
        }

        public static async Task<string> DeSerializeGenericFileAsync(string fileName)
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
