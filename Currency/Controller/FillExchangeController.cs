using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Currency.Model;

namespace Currency.Controller
{
    public class FillExchangeController
    {
        public SAPbobsCOM.Company Company;
        public SAPbouiCOM.IForm Form;
        public DateTime StartDate;
        public DateTime EndDate;
        public List<String> CurrList;
        public SAPbouiCOM.IForm ExchangeForm = Currency.Model.IForm.ExchangeForm;


        public SAPbobsCOM.SBObob rs { get { return (SAPbobsCOM.SBObob)Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoBridge); } }
        public SAPbouiCOM.ComboBox MonthChoice { get { return (SAPbouiCOM.ComboBox)ExchangeForm.Items.Item("13").Specific; } }
        public SAPbouiCOM.ComboBox YearChoice { get { return (SAPbouiCOM.ComboBox)ExchangeForm.Items.Item("12").Specific; } }

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

           


            Service.CurrencyService service = new Service.CurrencyService();
            foreach (var i in CurrList)
            {
                DateTime startDate = StartDate;
                for (int o = 0; startDate <= EndDate; startDate = StartDate.AddDays(o))
                {
                    currencies.Add(service.GetCurrencies(i, StartDate.AddDays(o)));
                    o++;
                }
            }





            var oProgressBar = SAPbouiCOM.Framework.Application.SBO_Application.StatusBar.CreateProgressBar(code, currencies.Count, true);


            try
            {
                var Progress = 0;
                oProgressBar.Text = "Fill Exchange Rates";
                var count = 0;
                var currCount = 0;
                DateTime DateTimeCur;
                var startDateForFill = StartDate;


                foreach (var i in currencies)
                    {

                    rate = i.currencies.Select(o => o.Rate).FirstOrDefault();
                        code = i.currencies.Select(o => o.Code).FirstOrDefault();
                    if(code!="" && code !=null)
                        rs.SetCurrencyRate(code.ToUpper(), startDateForFill, rate, true);
         

                    if (startDateForFill < EndDate)
                        startDateForFill = startDateForFill.AddDays(1);
                    else startDateForFill = StartDate;
                    Progress += 1;
                    oProgressBar.Value = Progress;

                }
                MonthChoice.Select(StartDate.Month - 2, SAPbouiCOM.BoSearchKey.psk_Index);
                MonthChoice.Select(StartDate.Month - 1, SAPbouiCOM.BoSearchKey.psk_Index);

               
                YearChoice.Select(StartDate.Year.ToString(), SAPbouiCOM.BoSearchKey.psk_ByValue);
                oProgressBar.Stop();
                return 1;
         

        
                
            }
            catch (Exception ex)
            {
                oProgressBar.Stop();
                SAPbouiCOM.Framework.Application.SBO_Application.MessageBox(ex.Message.ToString());
                return -1;
            }
           
        }
    }
}
