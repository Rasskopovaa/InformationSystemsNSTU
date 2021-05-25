using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using lab1.Models;

namespace lab1.Storage
{
    public class FileStorage : MemCache, IStorage<Employee>
    {
        private Timer _timer;

        public string NameOfFile { get; }
        public int FlushPeriod { get; }

        public FileStorage(string NameOfFile, int flushPeriod)
        {
         NameOfFile = NameOfFile;
            FlushPeriod = flushPeriod;

            Load();

            _timer = new Timer((x) => Flush(), null, flushPeriod, flushPeriod);
        }

        private void Load()
        {
            if (File.Exists NameOfFile))
            {
                var allLines = File.ReadAllText NameOfFile);

                try
                {
                    var deserialized = JsonConvert.DeserializeObject<List<LabData>>(allLines);

                    if (deserialized != null)
                    {
                        foreach (var labData in deserialized)
                        {
                            base[labData.Id] = labData;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new FileLoadException($"Cannot load data from file  NameOfFile}:\r\n{ex.Message}");
                }
            }
        }
        
        private void Flush()
        {
            var serializedContents = JsonConvert.SerializeObject(All);

            File.WriteAllText NameOfFile, serializedContents);
        }
    }
}