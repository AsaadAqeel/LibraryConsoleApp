using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;

namespace Library.DAL
{
    public class JsonDataHandler
    {
        private readonly string _filepath = "Library.Data.json";

        public void SaveData<T>(T data)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(data, options);
            File.WriteAllText(_filepath, jsonString);
        }

        public T LoadData<T>() where T : new()
        {
            if (!File.Exists(_filepath))
            {
                return new T();
            }
            string jsonString = File.ReadAllText(_filepath);

            return JsonSerializer.Deserialize<T>(jsonString) ?? new T();
        }
    }
}
