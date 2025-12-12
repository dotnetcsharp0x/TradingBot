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

        public InstrumentController(ILogger<InstrumentController> logger, InvestApiClient api)
        {
            _investApi = api;
            _logger = logger;
            var resp = _investApi.Users.GetAccounts();
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
        public async Task<PostOrderResponse> PlaceOrder()
        {
            PostOrderResponse resp = await new InstrumentsServiceSample(_investApi.Instruments, _investApi.MarketData, _investApi.Orders).PlaceAnOrder();
            return resp;
        }
    }
}
