using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Inventry_Management.Extension
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }
}
