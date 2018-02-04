using System;
using SQLite;

namespace Inventry_Management.Model
{
    public class MeterialModel
    {
        [PrimaryKey, AutoIncrement]
        public int S_no { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public string Description { get; set; }
    }
}
