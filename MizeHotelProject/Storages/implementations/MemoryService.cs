using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MizeHotelProject.Storages.interfaces;

namespace MizeHotelProject.Storages.implementations
{
    public class MemoryService<T> : IReadWriteAbleStorage<T> where T : class
    {
        private T _value { get; set; }
        private DateTime _expieryDate = DateTime.MinValue;
        private int _timeToLive;

        public MemoryService(int timeToLive)
        {
            _timeToLive = timeToLive;
        }

        public int GetTimeToLive => _timeToLive;

        public bool CanWrite => true;
        public async Task<T> GetValue()
        {
            Console.WriteLine("try retive data from memory service");
            if (DateTime.Now >  _expieryDate)
            {
                Console.WriteLine("data from memory service is expired");
                return null;
            }
            Console.WriteLine("data retrived from memory service");
            return _value;
        }

        public Task Overrite(T valueToWrite)
        {
            _value = valueToWrite;
            _expieryDate = DateTime.Now.AddHours(_timeToLive);
            Console.WriteLine($"update data in memory service, next expirey date {_expieryDate}");
            return Task.CompletedTask;
        }
    }
}
