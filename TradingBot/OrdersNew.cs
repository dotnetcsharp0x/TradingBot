using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBot
{
    public class OrdersNew
    {
        public class AveragePositionPrice
        {
            public string currency { get; set; }
            public int units { get; set; }
            public int nano { get; set; }
        }

        public class ExecutedCommission
        {
            public string currency { get; set; }
            public int units { get; set; }
            public int nano { get; set; }
        }

        public class ExecutedOrderPrice
        {
            public string currency { get; set; }
            public int units { get; set; }
            public int nano { get; set; }
        }

        public class InitialCommission
        {
            public string currency { get; set; }
            public int units { get; set; }
            public int nano { get; set; }
        }

        public class InitialOrderPrice
        {
            public string currency { get; set; }
            public int units { get; set; }
            public int nano { get; set; }
        }

        public class InitialSecurityPrice
        {
            public string currency { get; set; }
            public int units { get; set; }
            public int nano { get; set; }
        }

        public class Order
        {
            public string orderId { get; set; }
            public int executionReportStatus { get; set; }
            public int lotsRequested { get; set; }
            public int lotsExecuted { get; set; }
            public InitialOrderPrice initialOrderPrice { get; set; }
            public ExecutedOrderPrice executedOrderPrice { get; set; }
            public TotalOrderAmount totalOrderAmount { get; set; }
            public AveragePositionPrice averagePositionPrice { get; set; }
            public InitialCommission initialCommission { get; set; }
            public ExecutedCommission executedCommission { get; set; }
            public string figi { get; set; }
            public int direction { get; set; }
            public InitialSecurityPrice initialSecurityPrice { get; set; }
            public List<object> stages { get; set; }
            public ServiceCommission serviceCommission { get; set; }
            public string currency { get; set; }
            public int orderType { get; set; }
            public OrderDate orderDate { get; set; }
            public string instrumentUid { get; set; }
            public string orderRequestId { get; set; }
        }

        public class OrderDate
        {
            public int seconds { get; set; }
            public int nanos { get; set; }
        }

        public class Root
        {
            public List<Order> orders { get; set; }
        }

        public class ServiceCommission
        {
            public string currency { get; set; }
            public int units { get; set; }
            public int nano { get; set; }
        }

        public class TotalOrderAmount
        {
            public string currency { get; set; }
            public int units { get; set; }
            public int nano { get; set; }
        }
    }
}
