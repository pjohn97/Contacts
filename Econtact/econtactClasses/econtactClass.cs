using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Econtact.econtactClasses
{
    class econtactClass
    {
        //Getter Setter Properties
        //Acts as a data carrier
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        static string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        //Selecting data from Database
        public DataTable Select()
        {
            //Step 1:Database connection
            SqlConnection conn = new SqlConnection(myconnstring);
            DataTable dt = new DataTable();
            try
            {
                //Step 2:Writing SQL Query
                string sql = "SELECT * FROM tbl_contact";
                //Creating cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Creating SQL DataAdapter using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
               
                conn.Open();
                adapter.Fill(dt);


            }
            catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        //Inserting Data into Database
        public bool Insert(econtactClass c)
        {
            //Creating a default return type and setting its value to false
            bool isSuccess = false;

            //Step 1:Connect database
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                //Step 2:Create SQL Query
                string sql = "INSERT INTO tbl_contact(FirstName, LastName, ContactNo, Address, Gender) VALUES(@FirstName, @LastName, @ContactNo, @Address, @Gender)";
                //Creating cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Create parameters to add data
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);


                //Connection open here
                conn.Open();
                int row = cmd.ExecuteNonQuery();
                //if the query runs successfully, the value of rows will be greater than zero
                if(row>0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        //Method to update data in database from application
        public bool update(econtactClass c)
        {
            //Create a default retuen type and set default to false
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                //SQL to update data in our Database
                string sql = "UPDATE tbl_contact SET FirstName=@FirstName, LastName=@Lastname, ContactNo=@ContactNo, Address=@Address, Gender=@Gender WHERE ContactID=@ContactID";

                //Creating SQL Command
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Create Parameters to add value
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                cmd.Parameters.AddWithValue("ContactID", c.ContactID);
                //Open DAtabase Connection
                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                //if the query runs sucessfully then the value of rows will be greater than zero else its value will be zero
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        //Method to delete data from database
        public bool Delete(econtactClass c)
        {
            //Create a default retuen type and set default to false
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstring);
            try
            {
                //SQL To Delte DAta
                string sql = "DELETE FROM tbl_contact WHERE ContactID=@ContactID";

                //Creating SQL Command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);
                //Open Connection
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                //If the query run sucessfully then the value of rows is greater than zero else its value is 0
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                //Close Connection
                conn.Close();
            }
            return isSuccess;
        }
    }
}
