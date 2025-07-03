using SILCommServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace COMServer.Classes
{
    public enum EnumDbType { SELECT, INSERT, UPDATE, DELETE, SELECTBYID, SEARCH, VALIDATEUSER };
    public enum EnumAppType { DESKTOPAPP, COMMSERVER, ANDROIDAPP };
    public class clsMsgRule
    {
        public string sResponse = string.Empty;
        public static string sValid = "VALID";
        public static string sInValid = "INVALID";
        public static string sError = "ERROR";
        public static int sPort;
    }
}

