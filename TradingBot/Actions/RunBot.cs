using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tinkoff.InvestApi.V1;
using TradingBotService;
using static System.Net.Mime.MediaTypeNames;
using static TradingBot.SharePrices;

namespace TradingBot.Actions
{
    
    public class RunBot
    {
        private bool debug_mode = true; // Если true, то записи в базу данных не будет
        private bool decrease = true;
        static HttpClient httpClient = new HttpClient();
        private DDD.Root _shares { get; set; }
        private SharePrices.Root _prices { get; set; }
        public RunBot() { }
        public async Task WriteToFile(string path,string text)
        {
            await File.AppendAllTextAsync(path, text);
        }

        public async Task<double?> ReadOrders()
        {
            var resp = await httpClient.GetFromJsonAsync<OrdersNew.Root>("https://localhost:5001/api/Instrument/GetOrders");
            DirectoryInfo place = new DirectoryInfo(@"C:\Users\dmitry\source\repos\TradingBot\TradingBotService");
            FileInfo[] Files = place.GetFiles("*.txt");
            _shares = await httpClient.GetFromJsonAsync<DDD.Root>("https://localhost:5001/api/Instrument/GetInstrument");
            _prices = await httpClient.GetFromJsonAsync<SharePrices.Root>("https://localhost:5001/api/Instrument/GetOrderBook");
            var Tickers = (from share in _shares.instruments select share);
            if (resp.orders.Count() > 0 || Tickers.Count() > 0) 
            {
                foreach (FileInfo i in Files)
                {
                    var split_i = i.Name.Split("-");
                    string step_test = split_i[1].Replace(".txt","");
                    decimal step = Convert.ToDecimal(split_i[1].Replace(".txt", "")) /100;
                    var Tickerd = (from share in Tickers where share.ticker == split_i[0].Replace(".txt", "") select share).FirstOrDefault();
                    var Prices = (from share in _prices.lastPrices where share.instrumentUid == Tickerd.uid select share).FirstOrDefault();
                    if (Tickerd != null) {
                        foreach (string line in File.ReadLines(@"C:\Users\dmitry\source\repos\TradingBot\TradingBotService\" + i.Name))
                        {
                            decimal price = Decimal.Round(decimal.Parse(line),2);
                            if (Convert.ToDecimal(price) == Convert.ToDecimal(170.85))
                            {
                                Console.WriteLine("d");
                            }
                            int founded = 0;
                            var order_item = (from ord in resp.orders where ord.instrumentUid == Tickerd.uid select ord);
                            foreach (var order_price in order_item)
                            {
                                string pri = "";
                                int count = order_price.initialOrderPrice.nano.ToString().Count();
                                //MessageBox.Show(count.ToString());
                                pri = String.Format(order_price.initialOrderPrice.units.ToString() + "," + order_price.initialOrderPrice.nano.ToString());
                                
                                Instrument instrument = new Instrument();
                                PostOrderRequest order = new PostOrderRequest();
                                order.Price = price;
                                decimal orderPrice = Decimal.Round(decimal.Parse(pri), 2);
                                orderPrice = orderPrice / Tickerd.lot;
                                if (Convert.ToDecimal(orderPrice) == Convert.ToDecimal(170.85))
                                {
                                    Console.WriteLine("d");
                                }
                                if (Convert.ToDecimal(orderPrice) == Convert.ToDecimal(price))
                                {
                                    founded = 1;
                                }
                            }
                            if (founded == 0)
                            {
                                try
                                {
                                    string pri = "";
                                    pri = String.Format(Prices.price.units.ToString() + "," + Prices.price.nano.ToString());
                                    decimal orderPrice = Decimal.Round(decimal.Parse(pri), 2);
                                    //orderPrice = orderPrice / Tickerd.lot;
                                    if (orderPrice > price+(price/100*step))
                                    {
                                        await httpClient.GetFromJsonAsync<SharePrices.Root>("https://localhost:5001/api/Instrument/PlaceOrder?ticker=" + split_i[0].Replace(".txt", "") + "&price=" + price);
                                    }
                                    if (orderPrice < price-(price / 100 * step))
                                    {
                                        await httpClient.GetFromJsonAsync<SharePrices.Root>("https://localhost:5001/api/Instrument/PlaceOrderSell?ticker=" + split_i[0].Replace(".txt", "") + "&price=" + price);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                finally { }
                            }
                        }
                    }
                }
            }
            return 1;
        }
        ~RunBot () { }
    }
}
