using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Currency.Controller
{
    public class FillExchangeController
    {
        public SAPbobsCOM.Company Company;
        public SAPbouiCOM.IForm Form;
        public DateTime StartDate;
        public DateTime EndDate;
        public List<String> CurrList;

        public SAPbobsCOM.SBObob rs { get { return (SAPbobsCOM.SBObob)Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoBridge); } }

        public FillExchangeController(SAPbobsCOM.Company Company, SAPbouiCOM.IForm Form, DateTime StartDate, DateTime EndDate, List<String> CurrList)
        {
            this.Company = Company;
            this.Form = Form;
            this.StartDate = StartDate;
            this.CurrList = CurrList;
            this.EndDate = EndDate;
        }

        public int FillMatrix()
        {
            DateTime dateTime = DateTime.Now;
            dateTime = StartDate;
            string code = "";
            double rate = 0;
            List<Model.Currencies> currencies = new List<Model.Currencies>();
            try
            {

                
                Service.CurrencyService service = new Service.CurrencyService();
                foreach (var i in CurrList)
                {
                    DateTime startDate=StartDate;
                    for (int o = 0; startDate <= EndDate; startDate=StartDate.AddDays(o))
                    {
                        currencies.Add(service.GetCurrencies(i, StartDate.AddDays(o)));
                        o++;
                    }
                }
                
                var count = 0;
                var currCount = 0;
                DateTime DateTimeCur;


                var k1 = rs.GetItemList();
                foreach (var g in k1.Fields)
                {
                    var  t = g.ToString();
                }
                    foreach (var i in currencies)
                    {
                        rate = i.currencies.Select(o => o.Rate).FirstOrDefault();
                        code = i.currencies.Select(o => o.Code).FirstOrDefault();
                   
                        switch (code)
                        {
                            case "EUR":                         
                            rs.SetCurrencyRate(code, i.Date, rate,true);
                                break;
                            case "GEL":
                                rs.SetCurrencyRate(code, i.Date, rate,true);
                            break;
                            case "RUB":
                                rs.SetCurrencyRate(code, i.Date, rate,true);
                            break;
                            case "USD":
                                rs.SetCurrencyRate(code, i.Date, rate,true);
                            break;
                            default:

                                break;
                        }
                    
                }
                return 1;
            }
            catch (Exception ex)
            {
                SAPbouiCOM.Framework.Application.SBO_Application.MessageBox(ex.Message.ToString());
                return -1;
            }
           
        }
    }
}
