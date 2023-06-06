using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iProcedure.Model
{
    public class Unit
    {
        //[AutoIncrement]
        //public int Id { get; set; }

        [PrimaryKey]
        public string unit_name { get; set; }
    }
}
