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
        public void insert(Perm perm)
        {
            SqlCommand cmd = new SqlCommand("insert into perm(fileid,ownerid) values(@fileid,@ownerid)", con);
            cmd.Parameters.AddWithValue("@fileid", perm.fileid);
            cmd.Parameters.AddWithValue("@ownerid", perm.ownerid);
            cmd.ExecuteNonQuery();
        }
        public int insert(File file)
        {
            SqlCommand cmd = new SqlCommand("insert into files(filename,ownerid) output inserted.id values(@filename,@ownerid)", con);
            cmd.Parameters.AddWithValue("@filename", file.filename);
            cmd.Parameters.AddWithValue("@ownerid", file.ownerid);
            try
            {
            file.id = (int)cmd.ExecuteScalar();
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
            }
            return file.id;
        }
        public List<File> getFiles(Owner owner)
        {
            List<File> lf = new List<File>();
            SqlCommand cmd = new SqlCommand("select id,filename,files.ownerid from files, perm where id=fileid and perm.ownerid=@ownerid", con);
            cmd.Parameters.AddWithValue("@ownerid", owner.id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    lf.Add(new File(row));
                }
            }

            return lf;
        }
        public bool register(Owner user)
        {
            SqlCommand cmd = new SqlCommand("insert into owner(name,email,password) output inserted.id values(@name,@email,@password)", con);
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