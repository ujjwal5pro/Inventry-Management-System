using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Inventry_Management.Extension
{
    public interface IFileHelper
    {
        SQLiteConnection GetConnection();
    }
}
