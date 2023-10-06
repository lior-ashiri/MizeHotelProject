using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MizeHotelProject.Storages.interfaces
{
    public interface IReadableStorage<T> where T : class
    {
        public Task<T?> GetValue();
        
        public bool CanWrite { get; }
    }
}
