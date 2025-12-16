using System.Globalization;
using System.Net.Http;
using System.Text;
using Google.Protobuf.WellKnownTypes;
using Tinkoff.InvestApi;
using Tinkoff.InvestApi.V1;
using System.Configuration;

namespace TradingBotService;

public class InstrumentsServiceSample
{
    private readonly InstrumentsService.InstrumentsServiceClient _service;
    private readonly MarketDataService.MarketDataServiceClient _market;
    private readonly OrdersService.OrdersServiceClient _order;
    static HttpClient httpClient = new HttpClient();
    string pathfile = System.Configuration.ConfigurationManager.AppSettings["Path"];
    public InstrumentsServiceSample(InstrumentsService.InstrumentsServiceClient service, MarketDataService.MarketDataServiceClient market, OrdersService.OrdersServiceClient order)
    {
        _market = market;
        _service = service;
        _order = order;
    }
    public async Task WriteToFile(string path, string text)
    {
        await File.AppendAllTextAsync(path, text);
    }
    public async Task<GetLastPricesResponse> GetOrderBook()
    {
        GetLastPricesRequest price = new GetLastPricesRequest();
        price.LastPriceType = LastPriceType.LastPriceExchange;
        GetLastPricesResponse orderBook = await _market.GetLastPricesAsync(price);
        return orderBook;
    }
    public async Task<GetOrdersResponse> GetOrders(string account_id)
    {
        GetOrdersRequest orders = new GetOrdersRequest();
        orders.AccountId = account_id;
        return _order.GetOrders(orders);
    }
    public async Task<CancelOrderResponse> CancelOrder(string account_id, List<string> order_id, OrderIdType orderType)
    {
        CancelOrderRequest order = new CancelOrderRequest();
        CancelOrderResponse cancelOrderResponse = new CancelOrderResponse();
        foreach (var item in order_id)
        {
            order.AccountId = account_id;
            order.OrderId = item;
            order.OrderIdType = orderType;
            cancelOrderResponse = await _order.CancelOrderAsync(order);
            Thread.Sleep(1000);
        }
        return cancelOrderResponse;
    }
    public async Task<PostOrderResponse> PlaceAnOrder(string account_id, string ticker, decimal price, long quantity, OrderDirection orderType, OrderType orderType1)
    {
        Console.WriteLine(price);
        var share = (from i in _service.Shares().Instruments where i.Ticker == ticker select i).FirstOrDefault();

        PostOrderRequest order = new PostOrderRequest();
        PostOrderResponse? resp = new PostOrderResponse();
        Tinkoff.InvestApi.V1.GetTradingStatusRequest getTradingStatusesRequest = new Tinkoff.InvestApi.V1.GetTradingStatusRequest();
        getTradingStatusesRequest.InstrumentId = share.Uid;
        var trading_status = _market.GetTradingStatus(getTradingStatusesRequest);
        Console.WriteLine(Environment.NewLine);
        Console.WriteLine(SecurityTradingStatus.NormalTrading);
        Console.WriteLine(Environment.NewLine);
        if (trading_status.TradingStatus == SecurityTradingStatus.NormalTrading || trading_status.TradingStatus == SecurityTradingStatus.DealerNormalTrading
             || trading_status.TradingStatus == SecurityTradingStatus.OpeningAuctionPeriod)
        {
            Console.WriteLine("price: " + price);
            order.InstrumentId = share.Uid;
            Console.WriteLine("-8");
            order.AccountId = account_id;
            Console.WriteLine("-9");
            order.Quantity = quantity;
            Console.WriteLine("-10");
            order.Price = price;
            Console.WriteLine(order.Price.Units);
            Console.WriteLine(order.Price.Nano);
            order.Direction = orderType;
            order.OrderType = orderType1;
            order.TimeInForce = TimeInForceType.TimeInForceDay;
            order.PriceType = PriceType.Currency;
            order.ConfirmMarginTrade = false;
            Console.WriteLine("order sent");
            resp = _order.PostOrder(order);
        }
        else
        {
            Console.WriteLine(trading_status.TradingStatus);
        }
        return resp;
    }
    public async Task<bool> CreateAnOrder(string account_id, string ticker, decimal price_from, decimal price_to, decimal step, int qty)
    {
        GetLastPricesRequest price = new GetLastPricesRequest();
        price.LastPriceType = LastPriceType.LastPriceExchange;
        GetLastPricesResponse orderBook = await _market.GetLastPricesAsync(price);
        List<decimal> prices = new List<decimal>();
        int quantity = 0;
        decimal place_price = Decimal.Round(price_from,2);
        GetLastPricesRequest price1 = new GetLastPricesRequest();
        price.LastPriceType = LastPriceType.LastPriceExchange;
        GetLastPricesResponse orderBook1 = await _market.GetLastPricesAsync(price);
        var _shares = await httpClient.GetFromJsonAsync<DDD.Root>("https://localhost:5001/api/Instrument/GetInstrument");
        var Tickers = (from share in _shares.instruments select share);
        var Tickerd = (from share in Tickers where share.ticker == ticker select share).FirstOrDefault();
        var Prices = (from share in orderBook1.LastPrices where share.InstrumentUid == Tickerd.uid select share).FirstOrDefault();
        string pri = "";
        pri = String.Format(Prices.Price.Units.ToString() + "," + Prices.Price.Nano.ToString());
        decimal orderPrice = Decimal.Round(decimal.Parse(pri), 2);
        while (place_price < price_to && place_price + (place_price / 100 * step) < price_to)
        {
            prices.Add(place_price);
            place_price = Decimal.Round(place_price + (place_price/100*step),2);
            if (orderPrice < place_price)
            {
                quantity=quantity + qty;
            }
            await WriteToFile(pathfile + ticker + "-" + Convert.ToInt32(step*100) + "-" + qty + ".txt", place_price.ToString() + Environment.NewLine);
        }
        await PlaceAnOrder(account_id, ticker, 0, quantity, OrderDirection.Buy, OrderType.Market);
        return true;
    }
    public async Task<bool> GetOpenOrders()
    {

        return true;
    }
    public async Task<SharesResponse> GetInstrumentsDescriptionAsync(CancellationToken cancellationToken)
    {
        var shares = await _service.SharesAsync(cancellationToken);
        var etfs = await _service.EtfsAsync(cancellationToken);
        var bonds = await _service.BondsAsync(cancellationToken);
        var futures = await _service.FuturesAsync(cancellationToken);

        var dividends = new List<GetDividendsResponse>(3);
        foreach (var share in shares.Instruments.Take(dividends.Capacity))
        {
            var dividendsResponse = await _service.GetDividendsAsync(new GetDividendsRequest
            {
                Figi = share.Figi,
                From = share.IpoDate,
                To = Timestamp.FromDateTime(DateTime.UtcNow)
            }, cancellationToken: cancellationToken);
            dividends.Add(dividendsResponse);
        }

        var accruedInterests = new List<GetAccruedInterestsResponse>(3);
        foreach (var bond in bonds.Instruments.Take(accruedInterests.Capacity))
        {
            var accruedInterestsResponse = await _service.GetAccruedInterestsAsync(new GetAccruedInterestsRequest
            { Figi = bond.Figi, From = bond.PlacementDate, To = Timestamp.FromDateTime(DateTime.UtcNow) },
                cancellationToken: cancellationToken);
            accruedInterests.Add(accruedInterestsResponse);
        }

        var futuresMargin = new List<GetFuturesMarginResponse>(3);
        foreach (var future in futures.Instruments.Take(accruedInterests.Capacity))
        {
            var futureMargin = await _service.GetFuturesMarginAsync(new GetFuturesMarginRequest { Figi = future.Figi },
                cancellationToken: cancellationToken);
            futuresMargin.Add(futureMargin);
        }

        var tradingSchedulesResponse = await _service.TradingSchedulesAsync(new TradingSchedulesRequest
        {
            Exchange = "SPB",
            From = Timestamp.FromDateTime(DateTime.UtcNow.Date.AddDays(1)),
            To = Timestamp.FromDateTime(DateTime.UtcNow.Date.AddDays(3))
        }, cancellationToken: cancellationToken);


        return shares;
    }

    public async Task<SharesResponse> GetInstrumentsDescription()
    {
        var shares = await _service.SharesAsync();
        //var etfs = _service.Etfs();
        //var bonds = _service.Bonds();
        //var futures = _service.Futures();

        //var dividends = new List<GetDividendsResponse>(3);
        //foreach (var share in shares.Instruments.Take(dividends.Capacity))
        //{
        //    var dividendsResponse = _service.GetDividends(new GetDividendsRequest
        //    {
        //        Figi = share.Figi,
        //        From = share.IpoDate,
        //        To = Timestamp.FromDateTime(DateTime.UtcNow)
        //    });
        //    dividends.Add(dividendsResponse);
        //}

        //var accruedInterests = new List<GetAccruedInterestsResponse>(3);
        //foreach (var bond in bonds.Instruments.Take(accruedInterests.Capacity))
        //{
        //    var accruedInterestsResponse = _service.GetAccruedInterests(new GetAccruedInterestsRequest
        //    { Figi = bond.Figi, From = bond.PlacementDate, To = Timestamp.FromDateTime(DateTime.UtcNow) });
        //    accruedInterests.Add(accruedInterestsResponse);
        //}

        //var futuresMargin = new List<GetFuturesMarginResponse>(3);
        //foreach (var future in futures.Instruments.Take(accruedInterests.Capacity))
        //{
        //    var futureMargin = _service.GetFuturesMargin(new GetFuturesMarginRequest { Figi = future.Figi });
        //    futuresMargin.Add(futureMargin);
        //}

        //var tradingSchedulesResponse = _service.TradingSchedules(new TradingSchedulesRequest
        //{
        //    Exchange = "SPB",
        //    From = Timestamp.FromDateTime(DateTime.UtcNow.Date.AddDays(1)),
        //    To = Timestamp.FromDateTime(DateTime.UtcNow.Date.AddDays(3))
        //});

        return shares;
    }

    

    private class InstrumentsFormatter
    {
        private readonly IReadOnlyList<GetAccruedInterestsResponse> _accruedInterests;
        private readonly IReadOnlyList<Bond> _bonds;
        private readonly IReadOnlyList<GetDividendsResponse> _dividends;
        private readonly IReadOnlyList<Etf> _etfs;
        private readonly IReadOnlyList<Future> _futures;
        private readonly IReadOnlyList<GetFuturesMarginResponse> _futuresMargin;
        private readonly IReadOnlyList<Share> _shares;
        private readonly TradingSchedulesResponse _tradingSchedulesResponse;

        public InstrumentsFormatter(IReadOnlyList<Share> shares, IReadOnlyList<Etf> etfs,
            IReadOnlyList<Bond> bonds, IReadOnlyList<Future> futures,
            IReadOnlyList<GetDividendsResponse> dividends,
            IReadOnlyList<GetAccruedInterestsResponse> accruedInterests,
            IReadOnlyList<GetFuturesMarginResponse> futuresMargin, TradingSchedulesResponse tradingSchedulesResponse)
        {
            _shares = shares;
            _etfs = etfs;
            _bonds = bonds;
            _futures = futures;
            _dividends = dividends;
            _accruedInterests = accruedInterests;
            _futuresMargin = futuresMargin;
            _tradingSchedulesResponse = tradingSchedulesResponse;
        }

        public string Format()
        {
            var stringBuilder = new StringBuilder();

            foreach (var tradingSchedule in _tradingSchedulesResponse.Exchanges)
            {
                stringBuilder.AppendFormat("Trading schedule for exchange {0}: ", tradingSchedule.Exchange)
                    .AppendLine();
                foreach (var tradingDay in tradingSchedule.Days)
                    stringBuilder.AppendFormat("- {0} {1:working;0;non-working} {2} {3}", tradingDay.Date,
                            tradingDay.IsTradingDay.GetHashCode(), tradingDay.StartTime, tradingDay.EndTime)
                        .AppendLine();
            }

            stringBuilder.AppendFormat("Loaded {0} shares", _shares.Count)
                .AppendLine();
            for (var i = 0; i < 10; i++)
            {
                var share = _shares[i];
                stringBuilder.AppendFormat("- [{0}] {1}", share.Figi, share.Name)
                    .AppendLine();
                if (i < _dividends.Count)
                {
                    var dividendsCount = Math.Min(10, _dividends[i].Dividends.Count);

                    if (dividendsCount == 0) continue;

                    stringBuilder.AppendFormat("  Dividends:").AppendLine();
                    for (var j = 0; j < dividendsCount; j++)
                    {
                        var dividend = _dividends[i].Dividends[j];
                        stringBuilder.AppendFormat("  - {0} {1} {2}", (decimal)dividend.DividendNet,
                                dividend.DividendType, dividend.DeclaredDate)
                            .AppendLine();
                    }
                }
            }

            stringBuilder.AppendLine("...").AppendLine();

            stringBuilder.AppendFormat("Loaded {0} etfs", _etfs.Count)
                .AppendLine();
            for (var i = 0; i < 10; i++)
            {
                var etf = _etfs[i];
                stringBuilder.AppendFormat("- [{0}] {1}", etf.Figi, etf.Name)
                    .AppendLine();
            }

            stringBuilder.AppendLine("...").AppendLine();

            stringBuilder.AppendFormat("Loaded {0} bonds", _bonds.Count)
                .AppendLine();
            for (var i = 0; i < 10; i++)
            {
                var bond = _bonds[i];
                stringBuilder.AppendFormat("- [{0}] {1}", bond.Figi, bond.Name)
                    .AppendLine();

                if (i < _accruedInterests.Count)
                {
                    stringBuilder.AppendFormat("  Accrued Interest:").AppendLine();
                    var accruedInterestsCount = Math.Min(_accruedInterests[i].AccruedInterests.Count, 10);
                    for (var j = 0; j < accruedInterestsCount; j++)
                    {
                        var accruedInterest = _accruedInterests[i].AccruedInterests[j];
                        stringBuilder.AppendFormat("  - {0} {1}", accruedInterest.Date,
                                (decimal)accruedInterest.Nominal)
                            .AppendLine();
                    }
                }
            }

            stringBuilder.AppendLine("...").AppendLine();

            stringBuilder.AppendFormat("Loaded {0} futures", _futures.Count)
                .AppendLine();
            for (var i = 0; i < 10; i++)
            {
                var future = _futures[i];
                stringBuilder.AppendFormat("- [{0}] {1}", future.Figi, future.Name)
                    .AppendLine();

                if (i < _futuresMargin.Count)
                {
                    stringBuilder.AppendFormat("  Initial Margin On Buy: {0}",
                        (decimal)_futuresMargin[i].InitialMarginOnBuy).AppendLine();
                    stringBuilder.AppendFormat("  Initial Margin On Sell: {0}",
                        (decimal)_futuresMargin[i].InitialMarginOnSell).AppendLine();
                }
            }

            stringBuilder.AppendLine("...");

            return stringBuilder.ToString();
        }
    }
}