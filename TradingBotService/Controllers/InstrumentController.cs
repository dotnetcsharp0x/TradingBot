using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Tinkoff.InvestApi;
using Tinkoff.InvestApi.V1;

namespace TradingBotService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstrumentController : Controller
    {
        private readonly ILogger<InstrumentController> _logger;
        private readonly InvestApiClient _investApi;
        private readonly GetAccountsResponse _getAccountsResponse;

        public InstrumentController(ILogger<InstrumentController> logger, InvestApiClient api)
        {
            _investApi = api;
            _logger = logger;
            _getAccountsResponse = _investApi.Users.GetAccounts();
        }

        [HttpGet("GetInstrument")]
        public async Task<SharesResponse> GetInstrument()
        {
            SharesResponse resp = await new InstrumentsServiceSample(_investApi.Instruments,_investApi.MarketData,_investApi.Orders).GetInstrumentsDescription();
            return resp;
        }

        [HttpGet("GetOrderBook")]
        public async Task<GetLastPricesResponse> GetOrderBook()
        {
            GetLastPricesResponse resp = await new InstrumentsServiceSample(_investApi.Instruments, _investApi.MarketData, _investApi.Orders).GetOrderBook();
            return resp;
        }

        [HttpGet("PlaceOrder")]
        public async Task<PostOrderResponse> PlaceOrder(string ticker = "SBERP", string price = "282.8070", long quantity = 1, OrderDirection orderType = OrderDirection.Buy)
        {
            decimal parse_price = Convert.ToDecimal(price.Replace(".",","));
            PostOrderResponse resp = await new InstrumentsServiceSample(_investApi.Instruments, _investApi.MarketData, _investApi.Orders).PlaceAnOrder(_getAccountsResponse.Accounts.First().Id, ticker, parse_price, quantity, orderType);
            return resp;
        }

        [HttpGet("CreateGrid")]
        public async Task<bool> CreateGrid(string ticker = "SBERP", decimal price_from = 1, decimal price_to = 1, decimal step = 1)
        {
            var resp = await new InstrumentsServiceSample(_investApi.Instruments, _investApi.MarketData, _investApi.Orders).CreateAnOrder(_getAccountsResponse.Accounts.First().Id, ticker, price_from, price_to, step);
            return true;
        }

        [HttpGet("GetOrders")]
        public async Task<GetOrdersResponse> GetOrders()
        {
            var resp = await new InstrumentsServiceSample(_investApi.Instruments, _investApi.MarketData, _investApi.Orders).GetOrders(_getAccountsResponse.Accounts.First().Id);
            return resp;
        }
    }
}
