using System;
using System.Windows.Forms;

namespace DP_manager
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
                ExitWithError("No server address provided.");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(args[0]));
        }

        static void ExitWithError(string message)
        {
            Console.Error.WriteLine(message);
            Environment.Exit(0);
        }
    }
}
