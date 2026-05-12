using System;
using System.Windows.Forms;

namespace PROG6221_Assignment_Part2_ST10449059
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// Citation: Microsoft (2024) 'Application.Run Method' documentation.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Task 7: Global Error Handling to prevent unhandled crashes
            try
            {
                // To customize application configuration such as set high DPI settings or default font,
                // see https://aka.ms/applicationconfiguration.
                ApplicationConfiguration.Initialize();

                // Starts the GUI interface (Form1)
                Application.Run(new Form1());
            }
            catch (Exception ex)
            {
                // Professional fallback if the application fails to boot
                MessageBox.Show("A critical error occurred while starting CyberShield: " + ex.Message,
                                "System Boot Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}