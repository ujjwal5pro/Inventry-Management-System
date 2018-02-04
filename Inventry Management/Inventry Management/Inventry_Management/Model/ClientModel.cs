using SQLite;

namespace Inventry_Management.Model
{
    public class ClientModel
    {
        [PrimaryKey, AutoIncrement]
        public int S_no { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
