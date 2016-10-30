using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace csharpasst.Models
{
    public class Owner
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        int level { get; set; }
        public Owner(int id, string name, string email, string password, int level)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.password = password;
            this.level = level;
        }
        public Owner(string name, string email, string password)
        {
            this.id = -1;
            this.name = name;
            this.email = email;
            this.password = password;
            this.level = 0;
        }

        public Owner(DataRow row)
        {
            id = (int)row["id"];
            name = (string)row["name"];
            email = (string)row["email"];
            password = (string)row["password"];
            level = (int)row["level"];
        }

    }
}