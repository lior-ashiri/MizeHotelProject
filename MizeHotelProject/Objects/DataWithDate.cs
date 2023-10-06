using MizeHotelProject.Storages.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MizeHotelProject.objects
{
    public class DataWithDate<T> where T : class
    {
        public T Value { get; set; }
        public DateTime Date { get; set; }
    }
}
