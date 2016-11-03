using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using csharpasst.Models;
using System.Web.UI;
using NLog;

namespace csharpasst.Helpers
{
    public sealed class Database
    {
        private SqlConnection con;
        private static readonly Lazy<Database> lazy =
        new Lazy<Database>(() => new Database());
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static Database Instance { get { return lazy.Value; } }

        private Database()
        {
            try
            {
                logger.Info("Opening Database Connection");
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
                con.Open();

            }catch(Exception e)
            {
                logger.Fatal("Database connection not established");
                logger.Fatal(e.StackTrace);
            }

        }
        public void denyUser(string email, string filename)
        {
            SqlCommand cmd = new SqlCommand("delete from perm where ownerid=(select id from owner where email=@email) and fileid=(select id from files where filename=@filename)",con);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@filename", filename);
            try
            {
                cmd.ExecuteNonQuery();
                logger.Info(GlobalVariables.loggedInUser.email + " denied user " + email + " from file " + filename + " on ip " + GlobalVariables.GetIPAddress());
            }
                catch(SqlException e)
            {
                logger.Error(e.GetType().ToString() + "Exception on " + GlobalVariables.loggedInUser.email + " denied user " + email + " from file " + filename + " on ip " + GlobalVariables.GetIPAddress());
                logger.Error(e.StackTrace);
            }
        }
        public bool isAllowed(int id, string filename)
        {
            SqlCommand cmd = new SqlCommand("select count(*) from perm,files where files.id = perm.fileid and filename=@filename and perm.ownerid=@ownerid",con);
            cmd.Parameters.AddWithValue("@filename", filename);
            cmd.Parameters.AddWithValue("@ownerid", id);
            int k;
            string log = GlobalVariables.GetIPAddress() + " Accessed is allowed of  " + id + " for file " + filename;
            try
            {
                k = (int)cmd.ExecuteScalar();
                logger.Info(log);
            }
            catch (SqlException e)
            {
                k = 0;
                logger.Error(e.GetType().ToString() + "Exception on " + log);
                logger.Error(e.StackTrace);
            }
            return k > 0;
        }
        public void allowUser(string email, string filename)
        {
            SqlCommand cmd = new SqlCommand("insert into perm select distinct files.id,owner.id from owner,files where email=@email and filename=@filename", con);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@filename", filename);
            string log = GlobalVariables.GetIPAddress() + " allowed access to  " + email + " for file " + filename;
            try
            {
                cmd.ExecuteNonQuery();
                logger.Info(log);
            }
            catch (SqlException e)
            {
                logger.Error(e.GetType().ToString() + "Exception on " + log);
                logger.Error(e.StackTrace);
            }
        }
        public void insert(Perm perm)
        {
            SqlCommand cmd = new SqlCommand("insert into perm(fileid,ownerid) values(@fileid,@ownerid)", con);
            cmd.Parameters.AddWithValue("@fileid", perm.fileid);
            cmd.Parameters.AddWithValue("@ownerid", perm.ownerid);
            string log = GlobalVariables.GetIPAddress() + " added permission " + perm.ToString();
            try
            {
                cmd.ExecuteNonQuery();
                logger.Info(log);
            }
            catch (SqlException e)
            {
                logger.Error(e.GetType().ToString() + "Exception on " + log);
                logger.Error(e.StackTrace);
            }
        }
        public List<string> getDeniedUsers(string filename)
        {
            SqlCommand cmd = new SqlCommand("select email from owner where id not in (select owner.id from owner,files,perm where owner.id=perm.ownerid and files.id = perm.fileid and filename=@filename)", con);
            string log = GlobalVariables.GetIPAddress() + " accessed denied user list for " + filename;
            logger.Info(log);
            cmd.Parameters.AddWithValue("@filename", filename);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            List<string> lf = new List<string>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    lf.Add(row["email"].ToString());
                }
            }
            return lf;
        }
        public List<string> getAllowedUsers(string filename)
        {
            SqlCommand cmd = new SqlCommand("select email from owner,files,perm where owner.id=perm.ownerid and files.id = perm.fileid and filename=@filename and perm.ownerid!=files.ownerid",con);
            cmd.Parameters.AddWithValue("@filename", filename);
            string log = GlobalVariables.GetIPAddress() + " accessed Allowed user list for " + filename;
            logger.Info(log);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            List<string> lf = new List<string>();
            if(dt.Rows.Count>0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    lf.Add(row["email"].ToString());
                }
            }
            return lf;
        }
        public List<Owner> getUsers()
        {
            SqlCommand cmd = new SqlCommand("select * from owner",con);
            string log = GlobalVariables.GetIPAddress() + " accessed user list";
            logger.Info(log);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Owner> ol = new List<Owner>();
            if(dt.Rows.Count>0)
            {
                foreach (DataRow row in dt.Rows)
                    ol.Add(new Owner(row));
            }
            return ol;
        }
        public void givePermission(File file, Owner owner)
        {
            SqlCommand cmd = new SqlCommand("insert into perm values(@fileid,@ownerid)", con);
            cmd.Parameters.AddWithValue("@fileid", file.id);
            cmd.Parameters.AddWithValue("@ownerid", owner.id);
            string log = GlobalVariables.GetIPAddress() + " added permission for file " + file.ToString() + " to owner " + owner.ToString();
            try
            {
                cmd.ExecuteNonQuery();
                logger.Info(log);
            }
            catch (SqlException e)
            {
                logger.Error(e.GetType().ToString() + "Exception on " + log);
                logger.Error(e.StackTrace);
            }
        }
        public int insert(File file)
        {
            SqlCommand cmd = new SqlCommand("select sum(size) from files, owner where owner.id=files.ownerid and owner.id=@ownerid",con);
            cmd.Parameters.AddWithValue("@ownerid", file.ownerid);
            long k;
            string log = GlobalVariables.GetIPAddress() + " insert file " + file.ToString();
            try
            {
                k = Convert.ToInt64(cmd.ExecuteScalar());
                logger.Info(log);
            }
            catch (SqlException e)
            {
                k = 0;
                logger.Error(e.GetType().ToString() + "Exception on " + log);
                logger.Error(e.StackTrace);
            }
            if (k + file.size>Owner.quota)
            {
                System.Diagnostics.Debug.WriteLine("Quota Exceeded");
                return file.id;
            }
            cmd = new SqlCommand("insert into files(filename,ownerid,size) output inserted.id values(@filename,@ownerid,@size)", con);
            cmd.Parameters.AddWithValue("@filename", file.filename);
            cmd.Parameters.AddWithValue("@ownerid", file.ownerid);
            cmd.Parameters.AddWithValue("@size", file.size);
            try
            {
                file.id = (int)cmd.ExecuteScalar();
                logger.Info(log);
            }
            catch (Exception e)
            {
                logger.Error(e.GetType().ToString() + "Exception on " + log);
                logger.Error(e.StackTrace);
            }
            return file.id;
        }
        public List<File> getFiles(Owner owner)
        {
            List<File> lf = new List<File>();
            SqlCommand cmd = new SqlCommand("select id,filename,files.ownerid from files, perm where id=fileid and perm.ownerid=@ownerid", con);
            cmd.Parameters.AddWithValue("@ownerid", owner.id);
            string log = GlobalVariables.GetIPAddress() + " accessed files of " + owner.ToString();
            logger.Info(log);
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
            string log = GlobalVariables.GetIPAddress() + " registered " + user.ToString();
            try
            {
                user.id = (int)cmd.ExecuteScalar();
                logger.Info(log);
            }
            catch (Exception e)
            {
                user.id = -1;
                logger.Error(e.GetType().ToString() + "Exception on " + log);
                logger.Error(e.StackTrace);
            }
            return user.id > -1;
        }
        public Owner login(string email, string pass)
        {
            SqlCommand cmd = new SqlCommand("select * from owner where email=@email and Password=@password", con);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", pass);
            string log = GlobalVariables.GetIPAddress() + " tried login " + email;
            logger.Info(log);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return new Owner(dt.Rows[0]);
            }
            else
            {
                log = GlobalVariables.GetIPAddress() + " tried failed on " + email;
                logger.Info(log);
                return null;
            }
        }
    }
}