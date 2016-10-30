using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using csharpasst.Models;
using System.Web.UI;

namespace csharpasst.Helpers
{
    public sealed class Database
    {
        private SqlConnection con;
        private static readonly Lazy<Database> lazy =
        new Lazy<Database>(() => new Database());

        public static Database Instance { get { return lazy.Value; } }

        private Database()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            con.Open();
        }

        public bool register(Owner user)
        {
            SqlCommand cmd = new SqlCommand("insert into owner(name,email,password) values(@name,@email,@password)", con);
            cmd.Parameters.AddWithValue("@email", user.email);
            cmd.Parameters.AddWithValue("@password", user.password);
            cmd.Parameters.AddWithValue("@name", user.name);
            user.id = (int) cmd.ExecuteScalar();
            return user.id > -1;
        }
        public Owner login(string email, string pass)
        {
            SqlCommand cmd = new SqlCommand("select * from owner where email=@email and Password=@password", con);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", pass);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return new Owner(dt.Rows[0]);
            }
            else
            {
                return null;
            }
        }
    }
}