using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MSSQLProcGeneratorByKmeta
{
    public class SQLConn
    {
        /// <summary>
        /// Gets the columns in all tables in the DB into data table
        /// </summary>
        /// <param name="server">SQL Server Address</param>
        /// <param name="user">SQL Server User</param>
        /// <param name="password">SQL Server User Password</param>
        /// <param name="db">SQL Server Database</param>
        /// <param name="configurationFolderPath">Path to </param>
        /// <param name="columnsDataSql">SQL scriptfile name</param>
        /// <returns></returns>
        public DataTable GetProcTablesData(string server, string user, string password, string db, string configurationFolderPath, string columnsDataSql)
        {
            DataTable dataTbl = new DataTable();
            SqlConnection sqlConn = new SqlConnection();
            sqlConn.ConnectionString = string.Format("Server={0};User id={1};password={2};initial catalog={3};", server, user, password, db);
            FileInfo file = new FileInfo(configurationFolderPath + columnsDataSql);
            StreamReader SqlScriptReader = file.OpenText();
            StringBuilder sqlGetColumnsDataScript = new StringBuilder();
            sqlGetColumnsDataScript.Append(SqlScriptReader.ReadToEnd());

            try
            {
                SqlCommand sqlComm = new SqlCommand();
                sqlConn.Open();
                sqlComm = new SqlCommand(sqlGetColumnsDataScript.ToString(), sqlConn);
                SqlDataReader dbs = sqlComm.ExecuteReader();
                dataTbl.Load(dbs, LoadOption.OverwriteChanges);
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                sqlConn.Close();
                MessageBox.Show("Error Loading Tables Data and columns from the server. Error text: " + ex.Message);
                return null;
            }
            return dataTbl;
        }
        /// <summary>
        /// Checks the sever connection
        /// </summary>
        /// <param name="server">SQL Server Address</param>
        /// <param name="user">SQL User Name</param>
        /// <param name="password">SQL User Passowrd</param>
        /// <returns>true or false</returns>
        public bool ServerConnectionCeck(string server, string user, string password)
        {
            SqlConnection sqlConn = new SqlConnection();
            sqlConn.ConnectionString = string.Format("Server={0};User id={1};password={2};initial catalog=master;", server, user, password);
            string sqlquery = "use master;Select name from sys.databases ";
            try
            {
                SqlCommand sqlComm = new SqlCommand();
                sqlConn.Open();
                sqlComm = new SqlCommand(sqlquery, sqlConn);
                sqlComm.ExecuteNonQuery();
                sqlConn.Close();
                return true;
            }
            catch (Exception ex)
            {
                sqlConn.Close();
                MessageBox.Show("Error Connecting to the server. Error text: " + ex.Message);
                return false;
            }
        }
        /// <summary>
        /// Fills the combobox for databases   
        /// </summary>
        /// <param name="server">SQL Server Address</param>
        /// <param name="user">SQL User Name</param>
        /// <param name="password">SQL User Passowrd</param>
        public List<string> LoadDataBases(string server, string user, string password)
        {
            List<string> activedbs = new List<string>();
            SqlConnection sqlConn = new SqlConnection();
            sqlConn.ConnectionString = string.Format("Server={0};User id={1};password={2};initial catalog=master;", server, user, password);
            string sqlquery = "use master;Select [name] from sys.databases WHERE [state_desc] = 'ONLINE' Order BY [Name]";
            try
            {
                SqlCommand sqlComm = new SqlCommand();
                sqlConn.Open();
                sqlComm = new SqlCommand(sqlquery, sqlConn);
                SqlDataReader dbs = sqlComm.ExecuteReader();
                while (dbs.Read())
                {
                    activedbs.Add(dbs[0].ToString());
                }
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                sqlConn.Close();
                MessageBox.Show("Error Loading Databases from the server. Error text: " + ex.Message);
                return activedbs;
            }
            return activedbs;
        }
    }
}
