using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Scrapper scrapper = new Scrapper("https://9gag.com/");

            scrapper.Scrap(".jpg");
            scrapper.ExportDatas("D:\\pictures\\test9gag");
        }
    }
}
