using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; // ADO.NET Data Access Classes
using System.Data.SqlClient; // SQL Client Provider

namespace ARMSDALayer
{
    public class CreditCardDAO : ICreditCardDAO
    {
        public CreditCardDTO GetRecordByID(string key)
        {
            //Step 1-GET the Connection from SQLServerDAOFactory Object & Create ADO SqlConnection Object
            SqlConnection objConn = new SqlConnection(SQLServerDAOFactory.ConnectionString());
            //Step A-Start Error Trapping
            try
            {
                //Step 2-Open connection
                objConn.Open();
                //Step 3-Create SQL string

                string strSQL = "SELECT * FROM CreditCard WHERE CreditCardNumber = @CreditCardNumber;";
                Console.WriteLine(strSQL);

                //Step 4-Create Command object, pass query and connection object
                SqlCommand objCmd = new SqlCommand(strSQL, objConn);
                //Step 5-SET CommandType Property to text since we have a query string & NOT a Stored-Procedure
                //For stored procedures syntax is objCmd.CommandType = CommandType.StoredProJcedure;
                objCmd.CommandType = CommandType.Text;
                //Step 6-Add Parameter to. NOTE WE ARE ASSIGNING METHOD PARAMETER
                objCmd.Parameters.Add("@CreditCardNumber", SqlDbType.VarChar).Value = key;
                //Step 7-Create DATAREADER POINTER & Execute Query via
                //COMMAND OBJECT ExecuteReader Method which returns a populated
                //DATAREADER OBJECT with the results of the query
                SqlDataReader objDR = objCmd.ExecuteReader();
                //Step 8-Test to make sure there is data in the DataReader Object
                if (objDR.HasRows)
                {
                    //Create Data Transfer Object
                    CreditCardDTO objDTO = new CreditCardDTO();
                    //Step 8a-Call Read() Method to point and read the first record
                    objDR.Read();
                    //Step 8b-Extract data from a row s Object Populates itself.
                    //IMPORTANT! Note that data must be extracted in the ORDER
                    //in which the QUERY RETURNS THE DATA.
                    objDTO.CreditCardNumber = objDR.GetString(0);
                    objDTO.CreditCardOwnerName = objDR.GetString(1);
                    objDTO.CreditCardIssuingCompany = objDR.GetString(2);
                    objDTO.MerchantCode = objDR.GetString(3);
                    objDTO.ExpDate = objDR.GetDateTime(4);
                    objDTO.AddressLine1 = objDR.GetString(5);
                    objDTO.AddressLine2 = objDR.GetString(6);
                    objDTO.City = objDR.GetString(7);
                    objDTO.StateCode = objDR.GetString(8);
                    objDTO.ZipCode = objDR.GetString(9);
                    objDTO.Country = objDR.GetString(10);
                    objDTO.CreditCardLimit = objDR.GetDecimal(11);
                    objDTO.CreditCardBalance = objDR.GetDecimal(12);
                    objDTO.ActivationStatus = objDR.GetBoolean(13);
                    //Step 8b- Return Data Transfer Object
                    return objDTO;
                }
                //Step 9 - Terminate ADO Objects
                objDR.Close();
                objDR = null;
                objCmd.Dispose();
                objCmd = null;
                //Step10- return null since no data found
                return null;
            }//End of try
             //Step B-Trap for BO, App & General Exceptions
            catch (Exception objE)
            {
                //Step C- throw system exception since run time error has occurred.
                throw new Exception("Unexpected Error in CreditCardADO GetRecordByID(key) Method:{0} " + objE.Message);
            }
            finally
            {
                //Step 11-Terminate connection
                objConn.Close();
                objConn.Dispose();
                objConn = null;
            }
        }//End of GetRecordByID

        public bool Insert(CreditCardDTO objDTO)
        {
            //Step 1-GET the Connection from SQLServerDAOFactory Object & Create ADO SqlConnection Object
            SqlConnection objConn = new SqlConnection(SQLServerDAOFactory.ConnectionString());
            //Step A-Start Error Trapping
            try
            {
                //Step 2-Open connection
                objConn.Open();
                //Step 3-Create SQL string
                string strSQL;
                strSQL = "INSERT INTO CreditCard (CreditCardNumber,CreditCardOwnerName,MerchantCode,ExpDate,";
                strSQL = strSQL + "AddressLine1,AddressLine2,City,StateCode,ZipCode,Country,";
                strSQL = strSQL + "CreditCardBalance,CreditCardLimit,ActivationStatus)";
                strSQL = strSQL + "VALUES(@CreditCardNumber,@CreditCardOwnerName,@MerchantCode,@ExpDate,";
                strSQL = strSQL + "@AddressLine1,@AddressLine2,@City,@StateCode,@ZipCode,@Country,";
                strSQL = strSQL + "@CreditCardLimit,@CreditCardBalance,@ActivationStatus);";

                //Step 4-Create Command object, pass query and connection object
                SqlCommand objCmd = new SqlCommand(strSQL, objConn);
                //Step 5-SET CommandType Property to text since we have a query string & NOT a Stored-Procedure
                //For stored procedures syntax is objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandType = CommandType.Text;
                //Step 6-Add Parameter to. NOTE WE ARE ASSIGNING METHOD PARAMETER
                //IMPORTANT! Parameter TOKENS @XXXXX name must match same name Used in the INSERT QUERY
                //AND IN LISTED IN THE ORDER LISTED IN INSERT QUERY! NOTE WE ARE ASSIGNING ALL OBJECT'S DATA
                objCmd.Parameters.Add("@CreditCardNumber", SqlDbType.VarChar).Value = objDTO.CreditCardNumber;
                objCmd.Parameters.Add("@CreditCardOwnerName", SqlDbType.VarChar).Value = objDTO.CreditCardOwnerName;
                objCmd.Parameters.Add("@CreditCardIssuingCompany", SqlDbType.VarChar).Value = objDTO.CreditCardIssuingCompany;
                objCmd.Parameters.Add("@MerchantCode", SqlDbType.VarChar).Value = objDTO.MerchantCode;
                objCmd.Parameters.Add("@ExpDate", SqlDbType.DateTime).Value = objDTO.ExpDate;
                objCmd.Parameters.Add("@AddressLine1", SqlDbType.VarChar).Value = objDTO.AddressLine1;
                objCmd.Parameters.Add("@AddressLine2", SqlDbType.VarChar).Value = objDTO.AddressLine2;
                objCmd.Parameters.Add("@City", SqlDbType.VarChar).Value = objDTO.City;
                objCmd.Parameters.Add("@StateCode", SqlDbType.Char).Value = objDTO.StateCode.ToCharArray();
                objCmd.Parameters.Add("@ZipCode", SqlDbType.VarChar).Value = objDTO.ZipCode;
                objCmd.Parameters.Add("@Country", SqlDbType.VarChar).Value = objDTO.Country;
                objCmd.Parameters.Add("@CreditCardLimit", SqlDbType.Decimal).Value = objDTO.CreditCardLimit;
                objCmd.Parameters.Add("@CreditCardBalance", SqlDbType.Decimal).Value = objDTO.CreditCardBalance;
                objCmd.Parameters.Add("@ActivationStatus", SqlDbType.Bit).Value = objDTO.ActivationStatus;
                //Step 7-Execute ACTION-Query, Test result and throw exception if failed
                int intRecordsAffected = objCmd.ExecuteNonQuery();
                //Step 8-validate if INSERT QUERY was successful
                if (intRecordsAffected == 1)
                {
                    //Step 8a-Return true
                    return true;
                }
                //Step 9 - Terminate ADO Objects
                objCmd.Dispose();
                objCmd = null;
                //Step10-return false
                return false;
            }//End of try
             //Step B-Trap for BO, App & General Exceptions
            catch (Exception objE)
            {
                //Step C- throw system exception since run time error has occurred.
                throw new Exception("Unexpected Error in CreditCardADO Insert(CreditCardDTO objDTO) Method:{0} " + objE.Message);
            }
            finally
            {
                //Step 11-Terminate connection
                objConn.Close();
                objConn.Dispose();
                objConn = null;
            }
        }//End of Insert

        public bool Update(CreditCardDTO objDTO)
        {
            //Step 1-GET the Connection from SQLServerDAOFactory Object & Create ADO SqlConnection Object
            SqlConnection objConn = new SqlConnection(SQLServerDAOFactory.ConnectionString());
            //Step A-Start Error Trapping
            try
            {
                //Step 2-Open connection
                objConn.Open();
                //Step 3-Create SQL string
                string strSQL;
                strSQL = "UPDATE CreditCard";
                strSQL = strSQL + " SET CreditCardOwnerName=@CreditCardOwnerName,";
                strSQL = strSQL + "CreditCardIssuingCompany=@CreditCardIssuingCompany,";
                strSQL = strSQL + "MerchantCode=@MerchantCode,";
                strSQL = strSQL + "ExpDate=@ExpDate,";
                strSQL = strSQL + "AddressLine1=@AddressLine1,";
                strSQL = strSQL + "AddressLine2=@AddressLine2,";
                strSQL = strSQL + "City=@City,";
                strSQL = strSQL + "StateCode=@StateCode,";
                strSQL = strSQL + "ZipCode=@ZipCode,";
                strSQL = strSQL + "Country=@Country,";
                strSQL = strSQL + "CreditCardLimit=@CreditCardLimit,";
                strSQL = strSQL + "CreditCardBalance=@CreditCardBalance,";
                strSQL = strSQL + "ActivationStatus=@ActivationStatus";
                strSQL = strSQL + " WHERE CreditCardNumber=@CreditCardNumber;";

                //Step 4-Create Command object, pass query and connection object
                SqlCommand objCmd = new SqlCommand(strSQL, objConn);
                //Step 5-SET CommandType Property to text since we have a query string & NOT a Stored-Procedure
                //For stored procedures syntax is objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandType = CommandType.Text;
                //Step 6-Add Parameter to. NOTE WE ARE ASSIGNING METHOD PARAMETER
                //IMPORTANT! Parameter TOKENS @XXXXX name must match same name Used in the UPDATE QUERY
                //AND IN LISTED IN THE ORDER LISTED IN INSERT QUERY! NOTE WE ARE ASSIGNING ALL OBJECT'S DATA
                objCmd.Parameters.Add("@CreditCardOwnerName", SqlDbType.VarChar).Value = objDTO.CreditCardOwnerName;
                objCmd.Parameters.Add("@CreditCardIssuingCompany", SqlDbType.VarChar).Value = objDTO.CreditCardIssuingCompany;
                objCmd.Parameters.Add("@MerchantCode", SqlDbType.VarChar).Value = objDTO.MerchantCode;
                objCmd.Parameters.Add("@ExpDate", SqlDbType.VarChar).Value = objDTO.ExpDate;
                objCmd.Parameters.Add("@AddressLine1", SqlDbType.VarChar).Value = objDTO.AddressLine1;
                objCmd.Parameters.Add("@AddressLine2", SqlDbType.VarChar).Value = objDTO.AddressLine2;
                objCmd.Parameters.Add("@City", SqlDbType.VarChar).Value = objDTO.City;
                objCmd.Parameters.Add("@StateCode", SqlDbType.Char).Value = objDTO.StateCode.ToCharArray();
                objCmd.Parameters.Add("@ZipCode", SqlDbType.VarChar).Value = objDTO.ZipCode;
                objCmd.Parameters.Add("@Country", SqlDbType.VarChar).Value = objDTO.Country;
                objCmd.Parameters.Add("@CreditCardLimit", SqlDbType.Decimal).Value = objDTO.CreditCardLimit;
                objCmd.Parameters.Add("@CreditCardBalance", SqlDbType.Decimal).Value = objDTO.CreditCardBalance;
                objCmd.Parameters.Add("@ActivationStatus", SqlDbType.Bit).Value = objDTO.ActivationStatus;
                objCmd.Parameters.Add("@CreditCardNumber", SqlDbType.VarChar).Value = objDTO.CreditCardNumber;
                //Step 7-Execute ACTION-Query, Test result and throw exception if failed
                int intRecordsAffected = objCmd.ExecuteNonQuery();
                //Step 8-validate if INSERT QUERY was successful
                if (intRecordsAffected == 1)
                {
                    //Step 8a-Return true
                    return true;
                }
                //Step 9 - Terminate ADO Objects
                objCmd.Dispose();
                objCmd = null;
                //Step10-return false
                return false;
            }//End of try
             //Step B-Trap for BO, App & General Exceptions
            catch (Exception objE)
            {
                //Step C- throw system exception since run time error has occurred.
                throw new Exception("Unexpected Error in CreditCardADO Update(CreditCardDTO objDTO) Method:{0} " + objE.Message);
            }
            finally
            {
                //Step 11-Terminate connection
                objConn.Close();
                objConn.Dispose();
                objConn = null;
            }
        }//End of Update

        public bool Delete(string key)
        {
            //Step 1-GET the Connection from SQLServerDAOFactory Object & Create ADO SqlConnection Object
            SqlConnection objConn = new SqlConnection(SQLServerDAOFactory.ConnectionString());
            //Step A-Start Error Trapping
            try
            {
                //Step 2-Open connection
                objConn.Open();
                //Step 3-Create SQL string
                string strSQL = "DELETE FROM CreditCard WHERE CreditCardNumber = @CreditCardNumber;";

                //Step 4-Create Command object, pass query and connection object
                SqlCommand objCmd = new SqlCommand(strSQL, objConn);
                //Step 5-SET CommandType Property to text since we have a query string & NOT a Stored-Procedure
                //For stored procedures syntax is objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandType = CommandType.Text;
                //Step 6-Add Parameter to. NOTE WE ARE ASSIGNING METHOD PARAMETER
                //IMPORTANT! Parameter TOKENS @XXXXX name must match same name Used in the UPDATE QUERY
                //AND IN LISTED IN THE ORDER LISTED IN INSERT QUERY! NOTE WE ARE ASSIGNING ALL OBJECT'S DATA
                objCmd.Parameters.Add("@CreditCardNumber", SqlDbType.VarChar).Value = key;
                //Step 7-Execute ACTION-Query, Test result and throw exception if failed
                int intRecordsAffected = objCmd.ExecuteNonQuery();
                //Step 8-validate if INSERT QUERY was successful
                if (intRecordsAffected == 1)
                {
                    //Step 8a-Return true
                    return true;
                }
                //Step 9 - Terminate ADO Objects
                objCmd.Dispose();
                objCmd = null;
                //Step10-return false
                return false;
            }//End of try
             //Step B-Trap for BO, App & General Exceptions
            catch (Exception objE)
            {
                //Step C- throw system exception since run time error has occurred.
                throw new Exception("Unexpected Error in CreditCardADO Delete(key) Method:{0} " + objE.Message);
            }
            finally
            {
                //Step 11-Terminate connection
                objConn.Close();
                objConn.Dispose();
                objConn = null;
            }
        }//End of Delete

        public List<CreditCardDTO> GetAllRecords()
        {
            //Step 1-GET the Connection from SQLServerDAOFactory Object & Create ADO SqlConnection Object
            SqlConnection objConn = new SqlConnection(SQLServerDAOFactory.ConnectionString());
            //Step A-Start Error Trapping
            try
            {
                //Step 2-Open connection
                objConn.Open();
                //Step 3-Create SQL string
                string strSQL = "SELECT * FROM CreditCard;";

                //Step 4-Create Command object, pass query and connection object
                SqlCommand objCmd = new SqlCommand(strSQL, objConn);
                //Step 5-SET CommandType Property to text since we have a query string & NOT a Stored-Procedure
                //For stored procedures syntax is objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandType = CommandType.Text;
                //Step 7-Create DATAREADER POINTER & Execute Query via
                //COMMAND OBJECT ExecuteReader Method which returns a populated
                //DATAREADER OBJECT with the results of the query
                SqlDataReader objDR = objCmd.ExecuteReader();
                //Step 8-Test to make sure there is data in the DataReader Object
                if (objDR.HasRows)
                {
                    //Step 9-Test Create a Generic List Collection Object of Data Transfer Objects
                    List<CreditCardDTO> colRecordList = new List<CreditCardDTO>();
                    //Step 10-Loop through the Collection to Add Data Transfer Object
                    while (objDR.Read())
                    {
                        //10a-Create Data Transfer Object
                        CreditCardDTO objDTO = new CreditCardDTO();
                        //10b-Populate Data Transfer Object with DataReader records. IMPORTANT! Note that data
                        // must be extracted in the ORDER in which the QUERY
                        // RETURNS THE DATA.
                        objDTO.CreditCardNumber = objDR.GetString(0);
                        objDTO.CreditCardOwnerName = objDR.GetString(1);
                        objDTO.CreditCardIssuingCompany = objDR.GetString(2);
                        objDTO.MerchantCode = objDR.GetString(3);
                        objDTO.ExpDate = objDR.GetDateTime(4);
                        objDTO.AddressLine1 = objDR.GetString(5);
                        objDTO.AddressLine2 = objDR.GetString(6);
                        objDTO.City = objDR.GetString(7);
                        objDTO.StateCode = objDR.GetString(8);
                        objDTO.ZipCode = objDR.GetString(9);
                        objDTO.Country = objDR.GetString(10);
                        objDTO.CreditCardLimit = objDR.GetDecimal(11);
                        objDTO.CreditCardBalance = objDR.GetDecimal(12);
                        objDTO.ActivationStatus = objDR.GetBoolean(13);
                        //Step 10c-Add Data Transfer Object to the collection
                        colRecordList.Add(objDTO);
                    }//End of loop
                     //Step 11-Return the collection
                    return colRecordList;
                }
                else
                {
                    //Step 12 - Terminate ADO Objects
                    objDR.Close();
                    objDR = null;
                    objCmd.Dispose();
                    objCmd = null;
                    //Step13-return null since no records found
                    return null;

                }//End of if/else
            }//End of try
             //Step B-Trap for BO, App & General Exceptions
            catch (Exception objE)
            {
                //Step C- throw system exception since run time error has occurred.
                throw new Exception("Unexpected Error in CreditCardADO GetAllRecords() Method:{0} " + objE.Message);
            }
            finally
            {
                //Step 11-Terminate connection
                objConn.Close();
                objConn.Dispose();
                objConn = null;
            }
        }//End of GetAllRecords
    }

}