using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MizeHotelProject.Storages.interfaces
{
    public interface IReadWriteAbleStorage<T> : IReadableStorage<T> where T : class
    {
        public int GetTimeToLive { get; }
        public Task Overrite(T valueToWrite);
    }
}
