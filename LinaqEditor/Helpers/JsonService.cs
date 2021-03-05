using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinaqEditor.Helpers
{
    public static class JsonService
    {
        public static void SaveObjectToFile<T>(T obj, string filePath)
        {
            using (StreamWriter file = System.IO.File.CreateText(filePath))
            {
                file.Write(JsonConvert.SerializeObject(obj, Formatting.Indented));
            }
        }

        public static T GetObjectFromFile<T>(string FilePath)
        {
            return JsonConvert.DeserializeObject<T>(ReadJsonStorageContent(FilePath));
        }

        private static string ReadJsonStorageContent(string path)
        {
            if (File.Exists(path))
                return System.IO.File.ReadAllText(path);
            else
                return null;
        }
    }
}
