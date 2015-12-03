using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Keyence2IQS
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static DateTime EXP = new DateTime(2016, 7, 7, 0, 0, 0);
        //[STAThread]
        static void Main(string[] args)
        {
            if (EXP < DateTime.Now)
            {
                MessageBox.Show("Sorry, but this application is past its expiration date.  The application will now exit.");
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new dlgMain());
            }
        }
    }
}
