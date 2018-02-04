using System;
using System.IO;
using Inventry_Management.Droid;
using Inventry_Management.Extension;
using SQLite;
using Xamarin.Forms;

//using XamarinForms.SQLite.Droid.SQLite;
//using XamarinForms.SQLite.SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(FileHelper))]
namespace Inventry_Management.Droid
{
    public class FileHelper : IFileHelper
    {
        public FileHelper() { }
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "imDB.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, sqliteFilename);
            // Create the connection
            var conn = new SQLiteConnection(path);
            // Return the database connection
            return conn;
        }
    }
}