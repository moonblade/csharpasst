using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace csharpasst.Models
{
    public class File
    {
        public int id { get; set; }
        public string filename { get; set; }
        public int ownerid { get; set; }
        public File(string filename, int ownerid)
        {
            this.filename = filename;
            this.ownerid = ownerid;
        }
        public File(DataRow row)
        {
            this.id = (int)row["id"];
            this.filename = (string)row["filename"];
            this.ownerid = (int)row["ownerid"];
        }
    }
}