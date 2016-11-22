using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetVersionChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Get45or451FromRegistry();

                Console.WriteLine();
                Console.WriteLine("Press enter to exit");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static string CheckFor45DotVersion(int releaseKey)
        {
            if (releaseKey >= 394802)
            {
                return $"4.6.2 or later ({releaseKey})";
            }

            if (releaseKey >= 393297)
            {
                return $"4.6.1 or later ({releaseKey})";
            }

            if (releaseKey >= 393295)
            {
                return $"4.6 or later ({releaseKey})";
            }
            if ((releaseKey >= 379893))
            {
                return $"4.5.2 or later ({releaseKey})";
            }
            if ((releaseKey >= 378675))
            {
                return $"4.5.1 or later ({releaseKey})";
            }
            if ((releaseKey >= 378389))
            {
                return $"4.5 or later ({releaseKey})";
            }
            // This line should never execute. A non-null release key should mean
            // that 4.5 or later is installed.
            return $"No 4.5 or later version detected ({releaseKey})";
        }

        private static void Get45or451FromRegistry()
        {
            using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\"))
            {
                if (ndpKey != null && ndpKey.GetValue("Release") != null)
                {
                    Console.WriteLine("Version: " + CheckFor45DotVersion((int)ndpKey.GetValue("Release")));
                }
                else
                {
                    Console.WriteLine("Version 4.5 or later is not detected.");
                }
            }
        }
    }
}
