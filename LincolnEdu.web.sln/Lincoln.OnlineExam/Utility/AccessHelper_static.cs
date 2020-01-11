﻿using System.IO;
using System.Reflection;

namespace Lincoln.OnlineExam.Utility
{
    public partial class AccessHelper
    {
        static string get_defualt_dbpath()
        {
            return Path.GetDirectoryName(Assembly.GetAssembly(typeof(AccessHelper)).Location) + "\\db.mdb";
        } 
    }
}
