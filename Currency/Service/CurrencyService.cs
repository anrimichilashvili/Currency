using Currency.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Currency.Service
{
   public  class CurrencyService
    {
        private readonly string _serviceUrl;
        public CurrencyService(string url = "https://nbg.gov.ge/gw/api/ct/monetarypolicy/currencies")
        {
            _serviceUrl = url;
        }

        private static Currencies SendRequest(WebRequest webRequest)
        {

            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            if (response.StatusDescription == "OK")

            {
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<Currencies[]>(responseFromServer).FirstOrDefault();
            }
            throw new Exception(response.StatusDescription);
        }
        private string formatDate(DateTime dateTime)
        {
            return dateTime.AddDays(-1).ToString("yyyy-MM-dd");
        }
        public Currencies GetCurrencies()
        {
            WebRequest webRequest = WebRequest.Create(_serviceUrl);
            return SendRequest(webRequest);
        }

        public Currencies GetCurrencies(CurrencyEnums currencyEnum)
        {
            WebRequest webRequest = WebRequest.Create($"{_serviceUrl}/?currencies={currencyEnum}");
            return SendRequest(webRequest);
        }

        public Currencies GetCurrencies(DateTime dateTime)
        {
            WebRequest webRequest = WebRequest.Create($"{_serviceUrl}/?&date={formatDate(dateTime)}");
            return SendRequest(webRequest);
        }

        public Currencies GetCurrencies(string currencyCode, DateTime date)
        {
            WebRequest webRequest = WebRequest.Create($"{_serviceUrl}/?currencies={currencyCode}&date={(date)}");
            return SendRequest(webRequest);
        }
    }
}
