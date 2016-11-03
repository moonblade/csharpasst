using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace csharpasst.Models
{
    public class Perm
    {
        public int fileid { get; set; }
        public int ownerid { get; set; }
        public Perm(int fileid, int ownerid)
        {
            this.fileid = fileid;
            this.ownerid = ownerid;
        }
        public Perm(DataRow row)
        {
            this.fileid = (int)row["fileid"];
            this.ownerid = (int)row["ownerid"];
        }
        public override string ToString()
        {
            return ": " + this.fileid + " : " + this.ownerid + " :";
        }

    }
}