using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MizeHotelProject.objects;
using MizeHotelProject.Storages.interfaces;

namespace MizeHotelProject.Storages.implementations
{
    public class FileSystemService<T> : IReadWriteAbleStorage<T> where T : class
    {
        private readonly string _path;
        private DateTime _expieryDate = DateTime.MinValue;
        private int _timeToLiveInHours;
        public FileSystemService(string path, int timeToLiveInHours)
        {
            _path = path;
            _timeToLiveInHours = timeToLiveInHours;
        }
        public bool CanWrite => true;
        public int GetTimeToLive => _timeToLiveInHours;

        public async Task<T> GetValue()
        {
            Console.WriteLine("try retive data from fileSystem service");
            var jsonString = await File.ReadAllTextAsync(_path);
            Console.WriteLine(jsonString);
            var myObject = JsonSerializer.Deserialize<DataWithDate<T>>(jsonString);
            _expieryDate = myObject.Date;
            if (DateTime.Now > _expieryDate)
            {
                Console.WriteLine("data from fileSystem service expired");
                return null;
            }
            Console.WriteLine("data from fileSystem service retrived");
            return myObject.Value;
        }

        public async Task Overrite(T valueToWrite)
        {
            var date = DateTime.Now.AddHours(_timeToLiveInHours);
            var valueWithDate =new DataWithDate<T> { Date=date, Value=valueToWrite };
            var jsonString = JsonSerializer.Serialize(valueWithDate);
            await File.WriteAllTextAsync(_path, jsonString);
            _expieryDate = DateTime.Now;
            Console.WriteLine($"update data in storegeFile service, next expirey date {_expieryDate}");
        }
    }
}
