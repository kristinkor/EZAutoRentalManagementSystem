using ARMSBOLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARMSClientApp
{
    public partial class frmCreditCardSearchForm : Form
    {
        CreditCard objcCreditCard;
        public frmCreditCardSearchForm()
        {
            InitializeComponent();
        }






        private void btn_Print_Click(object sender, EventArgs e)
        {
                if (objcCreditCard != null)
                {
                    objcCreditCard.Print();
                    MessageBox.Show("Card Information has been saved to Network_Printer.txt");
                }
                else
                {
                    MessageBox.Show("Enter Credit Card Number in \"Credit Card Search\"section");
                
            }
        }

        private void btn_Exit_Click(object sender, EventArgs e) { 
            this.Close();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
                objcCreditCard = new CreditCard();
                bool success = objcCreditCard.Load(txt_Search.Text.Trim());
                Console.WriteLine(success);
                if (success)
                {
                    txt_CreditCardNumber.Text = objcCreditCard.CreditCardNumber;
                    txt_CreditCardOwnerName.Text = objcCreditCard.CreditCardOwnerName;
                    txt_CreditCardIssuingCompany.Text = objcCreditCard.CreditCardIssuingCompany;
                    txt_MerchantCode.Text = Convert.ToString(objcCreditCard.MerchantCode);
                    txt_ExpDate.Text = Convert.ToString(objcCreditCard.ExpDate);
                    txt_AddressLine1.Text = objcCreditCard.AddressLine1;
                    txt_AddressLine2.Text = objcCreditCard.AddressLine2;
                    txt_City.Text = objcCreditCard.City;
                    txt_State.Text = objcCreditCard.StateCode;
                    label21.Text = objcCreditCard.ZipCode;
                    txt_Country.Text = objcCreditCard.Country;
                    txt_CreditCardLimit.Text = Convert.ToString(objcCreditCard.CreditCardLimit);
                    txt_CreditCardBalance.Text = Convert.ToString(objcCreditCard.CreditCardBalance);
                    txt_ActivationStatus.Text = Convert.ToString(objcCreditCard.ActivationStatus);

                }//End of If
                else
                {
                    //Prompt user customer not found
                    MessageBox.Show("Credit Card Not Found ");

                    //Clear all controls
                    txt_CreditCardNumber.Text = "";
                    txt_CreditCardOwnerName.Text = "";
                    txt_CreditCardIssuingCompany.Text = "";
                    txt_MerchantCode.Text = " ";
                    txt_ExpDate.Text = "";
                    txt_AddressLine1.Text = "";
                    txt_AddressLine2.Text = "";
                    txt_City.Text = "";
                    txt_State.Text = "";
                    label21.Text = "";
                    txt_Country.Text = "";
                    txt_CreditCardLimit.Text = "";
                    txt_CreditCardBalance.Text = "";
                    txt_ActivationStatus.Text = "";
                }//end of else
            }
            catch (System.Exception)
            {
                MessageBox.Show("Error in search");
            }
        }
    }
}
