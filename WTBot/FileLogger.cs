using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTBot
{
   
    static class FileLogger
    {
        private const string filename = "logs.txt";
        public static void log(string message)
        {
            File.AppendAllText(filename, message);
        }
    }
}
