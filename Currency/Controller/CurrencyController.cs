using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Currency.Model;
using Currency.Service;

namespace Currency.Controller
{
    public class CurrencyController
    {

        public SAPbouiCOM.IForm Form;
        public SAPbobsCOM.Company Company;
        public SAPbouiCOM.Matrix Matrix { get { return (SAPbouiCOM.Matrix)Form.Items.Item("Curency_0").Specific; } }
        public SAPbouiCOM.DataTable DataTable { get { return (SAPbouiCOM.DataTable)Form.DataSources.DataTables.Item("DT_0"); } }


        public CurrencyController(SAPbobsCOM.Company company, SAPbouiCOM.IForm form)
        {
            Company = company;
            Form = form;
        }

        public void FillMatrix()
        {
            try
            {
                var recordSet = (SAPbobsCOM.Recordset)Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                recordSet.DoQuery("select * from TBCPayTest.dbo.OCRN where CurrCode not Like 'FT'");
               // recordSet.DoQuery("Select * from OCRN");
                var count = 0;
                DataTable.Rows.Add(recordSet.RecordCount);
                while (recordSet.EoF != true)
                {
                    var code = (string)recordSet.Fields.Item(0).Value;
                    DataTable.SetValue("Check", count, "N");
                    DataTable.SetValue("Currency", count++, code);
                    recordSet.MoveNext();
                }
                
                Matrix.LoadFromDataSource();
            }
            catch (Exception ex)
            {
                SAPbouiCOM.Framework.Application.SBO_Application.MessageBox(ex.Message.ToString());
            }
        }
        public void GetCheckboxValue()
        {
            var rs = (SAPbobsCOM.SBObob)Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoBridge);// sdk shi chavwero
            CurrencyService currserv = new CurrencyService();





            for (int i = 0; i < DataTable.Rows.Count; i++)
            {
                var checkbox = (string)DataTable.GetValue("Check", i);
                var currency = (string)DataTable.GetValue("Currency", i);
                if (checkbox == "Y")
                {
                    //test = currency;
                    Matrix.FlushToDataSource();
                    if (currency == "EUR")
                    {
                        var currencies = currserv.GetCurrencies(CurrencyEnums.EUR);
                        //Exchs.SetValue("Currency Rate", i, currencies.currencies[0]);
                        rs.SetCurrencyRate(currencies.currencies[0].Code, currencies.Date, currencies.currencies[0].Rate);
                    }
                    if (currency == "USD")
                    {
                        //CurrencyService currserv = new CurrencyService();
                        var usd = currserv.GetCurrencies(CurrencyEnums.USD);
                        rs.SetCurrencyRate(usd.currencies[0].Code, usd.Date, usd.currencies[0].Rate);
                    }
                    if (currency == "RUB")
                    {
                        //CurrencyService currserv = new CurrencyService();
                        var rub = currserv.GetCurrencies(CurrencyEnums.RUB);
                        rs.SetCurrencyRate(rub.currencies[0].Code, rub.Date, rub.currencies[0].Rate);
                    }

                }
            }
        }
    }
}
