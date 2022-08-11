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
        
    }
}
