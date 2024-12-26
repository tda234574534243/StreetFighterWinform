using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StreetFighterGame
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(CreateCenteredForm(new FormLogincs()));
        }
        private static Form CreateCenteredForm(Form form)
        {
            form.StartPosition = FormStartPosition.CenterScreen;
            return form;
        }
    }
}