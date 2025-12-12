namespace TradingBotService
{
    public class DDD
    {
        public class Brand
        {
            public string logoName { get; set; }
            public string logoBaseColor { get; set; }
            public string textColor { get; set; }
        }

        public class Dlong
        {
            public int units { get; set; }
            public int nano { get; set; }
        }

        public class DlongClient
        {
            public int units { get; set; }
            public int nano { get; set; }
        }

        public class DlongMin
        {
            public int units { get; set; }
            public int nano { get; set; }
        }

        public class Dshort
        {
            public int units { get; set; }
            public int nano { get; set; }
        }

        public class DshortClient
        {
            public int units { get; set; }
            public int nano { get; set; }
        }

        public class DshortMin
        {
            public int units { get; set; }
            public int nano { get; set; }
        }

        public class First1DayCandleDate
        {
            public int seconds { get; set; }
            public int nanos { get; set; }
        }

        public class First1MinCandleDate
        {
            public int seconds { get; set; }
            public int nanos { get; set; }
        }

        public class Instrument
        {
            public string figi { get; set; }
            public string ticker { get; set; }
            public string classCode { get; set; }
            public string isin { get; set; }
            public int lot { get; set; }
            public string currency { get; set; }
            public Klong klong { get; set; }
            public Kshort kshort { get; set; }
            public Dlong dlong { get; set; }
            public Dshort dshort { get; set; }
            public DlongMin dlongMin { get; set; }
            public DshortMin dshortMin { get; set; }
            public bool shortEnabledFlag { get; set; }
            public string name { get; set; }
            public string exchange { get; set; }
            public IpoDate ipoDate { get; set; }
            public object issueSize { get; set; }
            public string countryOfRisk { get; set; }
            public string countryOfRiskName { get; set; }
            public string sector { get; set; }
            public long issueSizePlan { get; set; }
            public Nominal nominal { get; set; }
            public int tradingStatus { get; set; }
            public bool otcFlag { get; set; }
            public bool buyAvailableFlag { get; set; }
            public bool sellAvailableFlag { get; set; }
            public bool divYieldFlag { get; set; }
            public int shareType { get; set; }
            public MinPriceIncrement minPriceIncrement { get; set; }
            public bool apiTradeAvailableFlag { get; set; }
            public string uid { get; set; }
            public int realExchange { get; set; }
            public string positionUid { get; set; }
            public string assetUid { get; set; }
            public int instrumentExchange { get; set; }
            public List<object> requiredTests { get; set; }
            public bool forIisFlag { get; set; }
            public bool forQualInvestorFlag { get; set; }
            public bool weekendFlag { get; set; }
            public bool blockedTcaFlag { get; set; }
            public bool liquidityFlag { get; set; }
            public First1MinCandleDate first1MinCandleDate { get; set; }
            public First1DayCandleDate first1DayCandleDate { get; set; }
            public Brand brand { get; set; }
            public DlongClient dlongClient { get; set; }
            public DshortClient dshortClient { get; set; }
        }

        public class IpoDate
        {
            public int seconds { get; set; }
            public int nanos { get; set; }
        }

        public class Klong
        {
            public int units { get; set; }
            public int nano { get; set; }
        }

        public class Kshort
        {
            public int units { get; set; }
            public int nano { get; set; }
        }

        public class MinPriceIncrement
        {
            public int units { get; set; }
            public int nano { get; set; }
        }

        public class Nominal
        {
            public string currency { get; set; }
            public int units { get; set; }
            public int nano { get; set; }
        }

        public class Root
        {
            public List<Instrument> instruments { get; set; }
        }
    }
}
