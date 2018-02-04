
using System.IO;
using Windows.Storage;
using SQLite;
//using XamarinForms.SQLite.SQLite;

namespace Inventry_Management.UWP
{
    public class SQLite_Uwp 
    {

        public SQLite_Uwp()
        {
        }

        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "MySQLiteDB.db3";
            string documentsPath = ApplicationData.Current.LocalFolder.Path;
            var path = Path.Combine(documentsPath, sqliteFilename);
            // Create the connection
            var conn = new SQLiteConnection(path);
            // Return the database connection
            return conn;
        }
    }
}
