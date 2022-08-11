
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
            Company = (SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();
        }
       
    }
}
