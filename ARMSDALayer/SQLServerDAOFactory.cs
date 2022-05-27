using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMSDALayer
{
    class SQLServerDAOFactory : DALObjectFactoryBase
    {
        /***********************************************************************/
        //Name: ConnectionString() Method
        //Purpose: Centralized method that returns the Connection String for
        // MS SQLServer data access.
        //Parameter: None.
        //Return Value: string that contains the connection string.
        public static string ConnectionString()
        {
            return "Data Source =.\\SQLExpress; Initial Catalog = EZRentalDB; Integrated Security = True";
        }

        /***********************************************************************/
        //Name: GetCreditCardDAO() Method
        //Purpose: Method that returns the CreditCardDAO Data Access Object
        // that handles the data access for the CreditCard
        // class in the business object Layer.
        //Parameter: None.
        //Return Value: a new CreditCardDAO object.
        public override CreditCardDAO GetCreditCardDAO()
        {
            //return CreditCardDAO Data Access Object to perform CreditCard class Data Access
            return new CreditCardDAO();
        }
    }
}