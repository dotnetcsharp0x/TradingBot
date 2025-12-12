using System.Net.Http.Json;
using TradingBotService;

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
            textBox5.Text = _shares.instruments.Count().ToString();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                await httpClient.GetFromJsonAsync<SharePrices.Root>("https://localhost:5001/api/Instrument/CreateGrid?ticker=" + textBox1.Text + "&price_from=" + textBox3.Text + "&price_to=" + textBox4.Text + "&step=" + textBox2.Text);
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
                    decimal pr_from = pr-(pr / 100 * 10);
                    decimal pr_to = pr+(pr / 100 * 10);
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
    }
}
