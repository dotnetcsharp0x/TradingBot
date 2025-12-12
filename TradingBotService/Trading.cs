using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tinkoff.InvestApi;
using Tinkoff.InvestApi.V1;

namespace TradingBotService
{
    public class Trading : BackgroundService
    {
        private readonly InvestApiClient _investApi;
        private readonly IHostApplicationLifetime _lifetime;
        private readonly ILogger<Trading> _logger;

        public Trading(ILogger<Trading> logger, InvestApiClient investApi, IHostApplicationLifetime lifetime)
        {
            _logger = logger;
            _investApi = investApi;
            _lifetime = lifetime;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var instrumentsDescription = new InstrumentsServiceSample(_investApi.Instruments,_investApi.MarketData, _investApi.Orders)
                    .GetInstrumentsDescription();

                var operationsDescription = new OperationsServiceSample(_investApi)
                    .GetOperationsDescription();
                _logger.LogInformation(operationsDescription);

                var tradingStatuses =
                    new MarketDataServiceSample(_investApi).GetTradingStatuses("a900c577-09e4-499b-b95a-f9983afa71aab");
                _logger.LogInformation(tradingStatuses);

                _lifetime.StopApplication();

                await Task.Delay(1, stoppingToken);
            }
        }
    }
}
