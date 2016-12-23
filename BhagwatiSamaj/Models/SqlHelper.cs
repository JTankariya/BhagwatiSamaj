using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BhagwatiSamaj.Models
{
    public class SqlHelper
    {
        private static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BhagwatiDB"].ToString());
        public static string MailFrom = ConfigurationManager.AppSettings["MailFrom"].ToString();
        public static string MailFromPassword = ConfigurationManager.AppSettings["MailFromPassword"].ToString();
        public static bool UseDefaultCredentials = Convert.ToBoolean(ConfigurationManager.AppSettings["MailUseDefaultCredentials"].ToString());
        public static bool EnableSSL = Convert.ToBoolean(ConfigurationManager.AppSettings["MailEnableSSL"].ToString());
        public static string MailHost = ConfigurationManager.AppSettings["MailHost"].ToString();
        public static int MailPort = Convert.ToInt32(ConfigurationManager.AppSettings["MailPort"].ToString());

        public static int ExecuteNonQuery(string commandText)
        {
            SqlCommand cmd = new SqlCommand(commandText, conn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            return cmd.ExecuteNonQuery();
        }

        public static object ExecuteScalar(string Query)
        {
            SqlCommand cmd = new SqlCommand(Query,conn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            object retval = cmd.ExecuteScalar();
            return retval;

        }

        public static DataTable GetDataTable(string commandText)
        {
            SqlCommand cmd = new SqlCommand(commandText, conn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            adp.Fill(dt);
            return dt;
        }

        public static List<T> ConvertToList<T>(DataTable dt)
        {
            try
            {
                var columnNames = dt.Columns.Cast<DataColumn>()
                    .Select(c => c.ColumnName.ToLower())
                    .ToList();

                var properties = typeof(T).GetProperties();

                return dt.AsEnumerable().Select(row =>
                {
                    var objT = Activator.CreateInstance<T>();

                    foreach (var pro in properties)
                    {
                        if (columnNames.Contains(pro.Name.ToLower()))
                            pro.SetValue(objT, row[pro.Name] == DBNull.Value ? null : row[pro.Name],null);
                    }

                    return objT;
                }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static IEnumerable<T> ConvertToEnumerable<T>(DataTable dt)
        {
            try
            {
                var columnNames = dt.Columns.Cast<DataColumn>()
                    .Select(c => c.ColumnName.ToLower())
                    .ToList();

                var properties = typeof(T).GetProperties();

                return dt.AsEnumerable().Select(row =>
                {
                    var objT = Activator.CreateInstance<T>();

                    foreach (var pro in properties)
                    {
                        if (columnNames.Contains(pro.Name.ToLower()))
                            pro.SetValue(objT, row[pro.Name] == DBNull.Value ? null : row[pro.Name], null);
                    }

                    return objT;
                }).AsEnumerable();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}