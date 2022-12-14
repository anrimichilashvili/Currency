
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
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_0").Specific));
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

        private void OnCustomInitialize()
        {
            Form = UIAPIRawForm;
            //Company = (SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();
        }

        private SAPbouiCOM.Button Button0;

        private void Button0_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;

            Model.IForm.ExchangeForm = Form;

            Form1 currencyForm = new Form1();
            currencyForm.Show();

        }
    }
}
