using MizeHotelProject.Storages.implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MizeHotelProject
{
    public class Consts
    {
        public const string myAppIDInOpenExchange = "217452e921dc4bb3b83518de43c9806f";
        public const string pathToOpehExchange = $"https://openexchangerates.org/api/latest.json?app_id=" + myAppIDInOpenExchange;
        public const string pathToJson = @"..\..\..\..\MizeHotelProject\data.Json";
        public const string DataSourceToTestOnlyPath = @"..\..\..\..\MizeHotelProject\dataSourceForTestingOnly.Json";
    }
}
