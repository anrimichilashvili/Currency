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

        public SAPbouiCOM.Matrix Matrix { get { return (SAPbouiCOM.Matrix)Form.Items.Item("4").Specific; } }
        public SAPbouiCOM.DataTable DataTable { get { return (SAPbouiCOM.DataTable)Form.DataSources.DataTables.Item("DT_0"); } }

        //public SAPbouiCOM.DBDataSource DBDataSource { get { return (SAPbouiCOM.DBDataSource)Form.DataSources.DBDataSources.Item("ORTT"); } }

        public SAPbouiCOM.DBDataSource DBDataSource { get { return (SAPbouiCOM.DBDataSource)Form.DataSources.DBDataSources.Item("ORTT"); } }

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

                Model.Currencies serviceResult= new Model.Currencies();
                var k = StartDate;
                
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


                    foreach (var i in currencies)
                    {
                        rate = i.currencies.Select(o => o.Rate).FirstOrDefault();
                        code = i.currencies.Select(o => o.Code).FirstOrDefault();
                        DateTimeCur = i.Date;

                    //  DateTime date2= DateTimeCur.AddDays(-1000);
                    //  if (date2 == DateTimeCur) continue;
                    //  date2 = DateTimeCur;
                    var l = rs.GetIndexRate("USD", DateTimeCur);
                        switch (code)
                        {
                            case "EUR":
                            rs.SetCurrencyRate(code, DateTimeCur, rate);
                                break;
                            case "GEL":
                                rs.SetCurrencyRate(code, DateTimeCur, rate);
                            break;
                            case "RUB":
                                rs.SetCurrencyRate(code, DateTimeCur, rate);
                            break;
                            case "USD":
                                rs.SetCurrencyRate(code, DateTimeCur, rate);
                            break;
                            default:

                                break;
                        }
                    
                }
               
              //  Matrix.LoadFromDataSource();
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
