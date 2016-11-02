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
        public long size { get; set; }
        public File(string filename, int ownerid, long size)
        {
            this.filename = filename;
            this.ownerid = ownerid;
            this.size = size;
        }
        public File(DataRow row)
        {
            this.id = (int)row["id"];
            this.filename = (string)row["filename"];
            this.ownerid = (int)row["ownerid"];
//            this.size = (int)row["size"];
        }
    }
}