using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RWA.Models.DAL
{
    public class DAL
    {
        public static DataTable ExecuteReader(string commandText, CommandType cmdType, params Parameter[] parameters) 
        {
            DataTable tbl = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString()))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = commandText;
                    if (cmdType==CommandType.SQL )
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                    }
                    else
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    }

                    foreach (var param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Name, param.Value);
                    }

                    //za čitanje podataka
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.HasRows)
                        {
                            tbl.Load(r);
                        }
                    }
                }
            }

            return tbl;
        }

        public static string ConnectionString() 
        {
            SqlConnectionStringBuilder cs = new SqlConnectionStringBuilder();
            cs.InitialCatalog = "AdventureWorksOBP";
            cs.DataSource = ".\\SQLEXPRESS2";
            cs.UserID = "sa";
            cs.Password = "sql";

            return cs.ConnectionString;
        }
    }
}