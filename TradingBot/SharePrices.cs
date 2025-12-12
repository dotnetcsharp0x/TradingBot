using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBot
{
    public class SharePrices
    {
        public class LastPrice
        {
            public string figi { get; set; }
            public Price price { get; set; }
            public Time time { get; set; }
            public string instrumentUid { get; set; }
            public int lastPriceType { get; set; }
        }

        public class Price
        {
            public int units { get; set; }
            public int nano { get; set; }
        }

        public class Root
        {
            public List<LastPrice> lastPrices { get; set; }
        }

        public class Time
        {
            public int seconds { get; set; }
            public int nanos { get; set; }
        }

    }
}
