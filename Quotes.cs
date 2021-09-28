using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyMicroservice
{
    public class Quotes
    {
        // "id": 1,
        //"propertyValueRangeFrom": 0,
        //"propertyValueRangeTo": 2,
        //"businessValueRangeFrom": 0,
        //"businessValueRangeTo": 2,
        //"propertyType": 0,
        //"quote": 30300
        public int id { get; set; }
        public int propertyValueRangeFrom { get; set; }

        public int propertyValueRangeTo { get; set; }

        public int businessValueRangeFrom { get; set; }

        public int businessValueRangeTo { get; set; }

        public string propertyType { get; set; }

        public string quote { get; set; }


    }
}
