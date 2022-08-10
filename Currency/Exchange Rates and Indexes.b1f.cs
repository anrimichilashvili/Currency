
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Currency.Controller;
using SAPbouiCOM.Framework;

namespace Currency
{

    [FormAttribute("866", "Exchange Rates and Indexes.b1f")]
    class Exchange_Rates_and_Indexes : SystemFormBase
    {
        public Exchange_Rates_and_Indexes()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Show_0").Specific));
            this.Button0.ClickBefore += new SAPbouiCOM._IButtonEvents_ClickBeforeEventHandler(this.Button0_ClickBefore);
            this.OnCustomInitialize();

        }
        public SAPbouiCOM.IForm Form;
        public SAPbobsCOM.Company Company;

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
        }

        private SAPbouiCOM.Button Button0;

        private void OnCustomInitialize()
        {
            Form = UIAPIRawForm;
            Company = (SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();
        }
        public Controller.CurrencyController CurrencyController;
        private void Button0_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            var wrongDateTime = "00010101";
            DateTime wrongDate = DateTime.ParseExact(wrongDateTime, "yyyyMMdd", CultureInfo.InvariantCulture);
            try
            {
                if(Model.CurrList.StartDate==wrongDate || Model.CurrList.currlist.Count == 0) 
                {
                    
                    Form1 form1 = new Form1();
                    form1.Show();
                    Application.SBO_Application.MessageBox("საჭიროა შეივსოს საწყისი ფორმა");
                    return;
                }
                FillExchangeController fillExchangeController = new FillExchangeController(Company, Form, Model.CurrList.StartDate, 
                    Model.CurrList.EndDate, Model.CurrList.currlist);
              
                CurrencyController = new CurrencyController((SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany(), 
                    UIAPIRawForm);
                //CurrencyController.GetCheckboxValue();
                fillExchangeController.FillMatrix();
            }
            catch (Exception ex)
            {
                Application.SBO_Application.MessageBox(ex.Message);
            }
        }
    }
}
