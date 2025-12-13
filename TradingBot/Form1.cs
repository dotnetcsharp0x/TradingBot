using System;
using System.Net.Http.Json;
using System.Text.Json;
using Tinkoff.InvestApi.V1;
using TradingBotService;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using static TradingBotService.Controllers.InstrumentController;

namespace TradingBot
{
    public partial class Form1 : Form
    {
        static HttpClient httpClient = new HttpClient();
        private DDD.Root _shares { get; set; }
        private SharePrices.Root _prices { get; set; }
        private string _figi = "";

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            _shares = await httpClient.GetFromJsonAsync<DDD.Root>("https://localhost:5001/api/Instrument/GetInstrument");
            _prices = await httpClient.GetFromJsonAsync<SharePrices.Root>("https://localhost:5001/api/Instrument/GetOrderBook");
            label13.Text = _shares.instruments.Count().ToString();
            DirectoryInfo place = new DirectoryInfo(@"C:\Users\dmitry\source\repos\TradingBot\TradingBotService");
            FileInfo[] Files = place.GetFiles("*.txt");
            foreach (FileInfo i in Files)
            {
                listBox1.Items.Add(i.Name.Replace(".txt", ""));
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                await httpClient.GetFromJsonAsync<SharePrices.Root>("https://localhost:5001/api/Instrument/CreateGrid?ticker=" + textBox1.Text.ToUpper() + "&price_from=" + textBox3.Text + "&price_to=" + textBox4.Text + "&step=" + textBox2.Text);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string ticker = textBox1.Text.ToUpper();
                var resp = (from i in _shares.instruments where i.ticker == ticker select i);
                if (resp.Count() > 0)
                {
                    _figi = resp.FirstOrDefault().figi;
                    var price = (from i in _prices.lastPrices where i.figi == _figi select i);
                    label7.Text = resp.FirstOrDefault().name;
                    decimal pr = Convert.ToDecimal(price.FirstOrDefault().price.units.ToString() + "," + price.FirstOrDefault().price.nano.ToString());
                    decimal pr_from = pr - (pr / 100 * 10);
                    decimal pr_to = pr + (pr / 100 * 10);
                    label9.Text = pr.ToString();
                    textBox3.Text = pr_from.ToString();
                    textBox4.Text = pr_to.ToString();
                }

            }
            catch (Exception ex)
            {
            }
            finally { }
        }

        private async void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            _shares = await httpClient.GetFromJsonAsync<DDD.Root>("https://localhost:5001/api/Instrument/GetInstrument");
            var Tickers = (from share in _shares.instruments select share);
            var resp = await httpClient.GetFromJsonAsync<OrdersNew.Root>("https://localhost:5001/api/Instrument/GetOrders");
            foreach (string line in File.ReadLines(@"C:\Users\dmitry\source\repos\TradingBot\TradingBotService\" + listBox1.Text + ".txt"))
            {
                decimal price = Decimal.Round(decimal.Parse(line), 2);
                string pri = "";
                var ticker = listBox1.Text.Split("-");
                var Tickerd = (from share in Tickers where share.ticker == ticker[0] select share).FirstOrDefault();
                //
                var order_item = (from ord in resp.orders where ord.instrumentUid == Tickerd.uid && Convert.ToDecimal(Convert.ToDecimal(String.Format(ord.initialOrderPrice.units.ToString() + "," + ord.initialOrderPrice.nano.ToString())) / Tickerd.lot) == Convert.ToDecimal(line) select ord).FirstOrDefault();
                if (order_item != null)
                {
                    pri = String.Format(order_item.initialOrderPrice.units.ToString() + "," + order_item.initialOrderPrice.nano.ToString());
                    pri = (Convert.ToDecimal(pri) / Tickerd.lot).ToString();
                
                    if (order_item.direction == 1)
                    {
                        listBox2.Items.Add(price.ToString());
                    }
                    if (order_item.direction == 2)
                    {
                        listBox3.Items.Add(price.ToString());
                    }
                }
                else
                {
                    listBox4.Items.Add(price.ToString());
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RefreshOrders();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            List<string> orders = new List<string>();
            _shares = await httpClient.GetFromJsonAsync<DDD.Root>("https://localhost:5001/api/Instrument/GetInstrument");
            var Tickers = (from share in _shares.instruments select share);
            OrderType op = new OrderType();
            var resp = await httpClient.GetFromJsonAsync<OrdersNew.Root>("https://localhost:5001/api/Instrument/GetOrders");
            foreach (string line in File.ReadLines(@"C:\Users\dmitry\source\repos\TradingBot\TradingBotService\" + listBox1.Text + ".txt"))
            {
                decimal price = Decimal.Round(decimal.Parse(line), 2);
                string pri = "";
                var Tickerd = (from share in Tickers where share.ticker == listBox1.Text select share).FirstOrDefault();
                //
                var order_item = (from ord in resp.orders where ord.instrumentUid == Tickerd.uid && Convert.ToDecimal(Convert.ToDecimal(String.Format(ord.initialOrderPrice.units.ToString() + "," + ord.initialOrderPrice.nano.ToString())) / Tickerd.lot) == Convert.ToDecimal(line) select ord).FirstOrDefault();
                if (order_item != null)
                {
                    pri = String.Format(order_item.initialOrderPrice.units.ToString() + "," + order_item.initialOrderPrice.nano.ToString());
                    pri = (Convert.ToDecimal(pri) / Tickerd.lot).ToString();
                    orders.Add(order_item.orderId.ToString());
                    op = (OrderType)order_item.orderType;
                }
            }
            if (orders.Count > 0)
            {
                CancelOrderView orderView = new CancelOrderView();
                orderView.orderType = op;
                orderView.order_id = orders;

                using (var response = await httpClient.PostAsJsonAsync("https://localhost:5001/api/Instrument/CancelOrder", orderView))
                {

                }
            }
            File.Delete(@"C:\Users\dmitry\source\repos\TradingBot\TradingBotService\" + listBox1.Text + ".txt");
            RefreshOrders();
        }
        private void RefreshOrders()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();
            DirectoryInfo place = new DirectoryInfo(@"C:\Users\dmitry\source\repos\TradingBot\TradingBotService");
            FileInfo[] Files = place.GetFiles("*.txt");
            foreach (FileInfo i in Files)
            {
                listBox1.Items.Add(i.Name.Replace(".txt", ""));
            }
        }
    }
}
