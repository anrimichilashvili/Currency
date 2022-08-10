﻿using SAPbouiCOM.Framework;
using System;
using System.Collections.Generic;
using System.Xml;
using Currency.Controller;
using System.Globalization;
using Currency.Service;
using System.Linq;

namespace Currency
{
    [FormAttribute("Currency.Form1", "Form1.b1f")]
    class Form1 : UserFormBase
    {
        public Form1()
        {
        }

        public CurrencyController CurrencyController;

        public SAPbouiCOM.IForm Form;
        public SAPbobsCOM.Company Company;
        
        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Matrix0 = ((SAPbouiCOM.Matrix)(this.GetItem("Curency_0").Specific));
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("stDate_0").Specific));
            this.EditText0.KeyDownAfter += new SAPbouiCOM._IEditTextEvents_KeyDownAfterEventHandler(this.EditText0_KeyDownAfter);
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("endDate_0").Specific));
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_3").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_4").Specific));
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_5").Specific));
            this.Button0.ClickBefore += new SAPbouiCOM._IButtonEvents_ClickBeforeEventHandler(this.Button0_ClickBefore);
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("Item_6").Specific));
            this.Button1.ClickBefore += new SAPbouiCOM._IButtonEvents_ClickBeforeEventHandler(this.Button1_ClickBefore);
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
           
        }

        private SAPbouiCOM.Matrix Matrix0;

        private void OnCustomInitialize()
        {
            Form = UIAPIRawForm;
            Company = new SAPbobsCOM.Company();
            CurrencyController = new CurrencyController((SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany(), UIAPIRawForm);
            CurrencyController.FillMatrix();
            Matrix0.AutoResizeColumns();

        }

        private SAPbouiCOM.EditText EditText0;
        private SAPbouiCOM.EditText EditText1;
        private SAPbouiCOM.StaticText StaticText0;
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.Button Button0;
        private SAPbouiCOM.Button Button1;

        private void Button1_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            EditText0.String = "";
            EditText1.String = "";
            UIAPIRawForm.Close();

        }

        private void Button0_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            var startDate = EditText0.String;
            var endDate = EditText1.String;
;
            

            if (EditText1.String == "" || EditText0.String == "")
            {
                Application.SBO_Application.MessageBox("ყველა ველი სავალდებულოა",1,"OK","","");
                return;
            }
            try
            {
               


                SAPbouiCOM.Matrix oMatrix = (SAPbouiCOM.Matrix)Form.Items.Item("Curency_0").Specific;
                
            
                


                var Matrix = (SAPbouiCOM.Matrix)(UIAPIRawForm.Items.Item("Curency_0").Specific);
               
                for (int i = 1; i <= CurrencyController.DataTable.Rows.Count; i++)
                {
                    
                    SAPbouiCOM.CheckBox oChkBox = (SAPbouiCOM.CheckBox)oMatrix.Columns.Item("Check_0").Cells.Item(i).Specific;
                    if (oChkBox.Checked == true)
                    {
                        SAPbouiCOM.EditText oEditItemCode = (SAPbouiCOM.EditText)oMatrix.Columns.Item("Currency_0").Cells.Item(i).Specific;
                        var currencyCode = oEditItemCode.Value;
                        Model.CurrList.currlist.Add(currencyCode);
                    }
                  
                  
                }


               


                var startingdate = DateTime.ParseExact(EditText0.Value, "yyyyMMdd", CultureInfo.InvariantCulture);
                var endingdate = DateTime.ParseExact(EditText1.Value, "yyyyMMdd", CultureInfo.InvariantCulture);
                Model.CurrList.StartDate = startingdate;
                Model.CurrList.EndDate = endingdate;


                Company = (SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();
               // var EXForm = Application.SBO_Application.Forms.Item("866");

                if (startingdate > endingdate)
                {
                    Application.SBO_Application.MessageBox("საწყისი თარიღი არ უნდა აჭარბებდეს საბოლოო თარიღს");
                    return;
                }
                if (endingdate > DateTime.Now)
                {
                    Application.SBO_Application.MessageBox("საბოლოო თარიღი არ უნდა აღემატებოდეს დღევანდელს");
                    return;
                }
                //CurrencyController.GetCheckboxValue();
                FillExchangeController fillExchangeController = new FillExchangeController(Company,Form,startingdate,endingdate, Model.CurrList.currlist);
                var result = fillExchangeController.FillMatrix();
                if(result==1)
                Application.SBO_Application.MessageBox("Open Exchange Rates and Index");
                
            }
            catch (Exception ex)
            {
                Application.SBO_Application.MessageBox(ex.Message.ToString(), 1, "Ok", "", "");
                Model.CurrList.StartDate = DateTime.Now;
                Model.CurrList.currlist.Clear();
            }
        }

        private void EditText0_KeyDownAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
       {
            throw new System.NotImplementedException();
        }
    }
}