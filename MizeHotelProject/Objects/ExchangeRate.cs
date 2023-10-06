using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MizeHotelProject.objects
{
    public class ExchangeRate
    {
        public string disclaimer { get; set; }
        public string license { get; set; }
        public long timestamp { get; set; }
        public string @base { get; set; }
        public Dictionary<string, decimal> rates { get; set; }
    }
}