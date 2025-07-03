using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COMServer
{
    static class Program
    {
        public static string PrinterName = "";
        public static string MachiningPrinterIP = "";
        public static string MachiningPrinterPort="";
        public static string FinalPackingPrinterIP = "";
        public static string FinalPackingPrinterPort = "";
        public static string MachiningPrnName = "MACHINING.prn";
        public static string TrolleyBox = "TROLLEYBOX.prn";
        public static string FinalPackingPrnName = "FINALPACKING.prn";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           
            bool CreatedOn;
            var mutex = new System.Threading.Mutex(true, "SatoCOMServer", out CreatedOn);
            if (!CreatedOn)
            {
                MessageBox.Show("Comm Server already running", "SatoCOMServer", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                return;
            }
            else
            { Application.Run(new frmServer()); }
        }
    }
}
