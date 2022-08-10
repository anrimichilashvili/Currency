using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Currency.Model;


namespace Currency.Service
{
    public interface ICurrencyService
    {
        Currencies GetCurrencies();

        Currencies GetCurrencies(CurrencyEnums currencyEnum);

        Currencies GetCurrencies(DateTime dateTime);

        Currencies GetCurrencies(string currencyCode);

    }
}
