using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace TestReturnCMD
{
    class Program
    {
        public static void Main(string[] args)
        {
            test();
            Console.WriteLine("...");
            Console.ReadKey();
        }
        public static void test()
        {
            var teste = ExecuteCmdCommand(@"netsh interface ip show config");

            string s = teste2(teste);
            ExecuteCmdCommand($@"netsh interface ipv4 set dnsservers {s} static 8.8.8.8 primary");
            ExecuteCmdCommand($@"netsh interface ipv4 add dnsservers {s} static 8.8.4.4 index=2");

        }
        public static string ExecuteCmdCommand(string commandLine)
        {
            Process process = new Process();
            return CmdCommand(process, commandLine);
        }
        private static string CmdCommand(Process process, string commandLine)
        {
            List<string> teste = new List<string>();
            ProcessStartInfo startInfo = new ProcessStartInfo("cmd");
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardError = true;
            var a = Process.Start(startInfo);
            a.StandardInput.WriteLine(commandLine);
            a.StandardInput.WriteLine("exit");
            string s = a.StandardOutput.ReadToEnd();


            Console.WriteLine(s);
            return s;
        }

        public static string teste2(string s)
        {
            var b = s.Split('\n');
            s = b[5];
            //"Configuração da interface \"Ethernet\"\r"
            for (int i = 0; i < 3; i++)
            {
                int pos = s.IndexOf(@" ");
                s = s.Remove(0, pos + 1);
            }
            return s;
        }
    }
}
