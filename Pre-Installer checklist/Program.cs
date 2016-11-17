using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pre_Installer_checklist
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("********** Operating System Information ***********");
            Console.WriteLine("Name = \"{0}\"", OSInfo.Name);
            Console.WriteLine("Version = \"{0}\"", OSInfo.Version);           
            Console.WriteLine("Service pack = \"{0}\"", OSInfo.ServicePack);
            Console.WriteLine("********** End ***********");
            Console.WriteLine();
            Console.WriteLine("********** MS Office Information ***********");
            Console.WriteLine("Version = \"{0}\"", MSOfficeInfo._Office_version);
            Console.WriteLine("Type (bitness) = \"{0}\"", MSOfficeInfo._Office_version_Bitness);
            Console.WriteLine("********** End ***********");
            Console.WriteLine();
            Console.WriteLine("********** User Access Control Information ***********");
            Console.WriteLine(UACInfo.StatusOfUAC);
            Console.WriteLine("********** End ***********");
            Console.WriteLine();
            Console.WriteLine("********** MS SQL Information ***********");
            Console.WriteLine("Microsoft SQL is \"{0}\"", MSSqlInfo.StatusOfMSSqlInstallation);
            MSSqlInfo.GetSqlServerInstances();
            MSSqlInfo.GetSqlInstances();
            Console.WriteLine("********** End ***********");
            Console.WriteLine();
            Console.WriteLine("********** Information related to RAM ***********");
            RAMInfo.GetInstalledMemory();
            //RAMInfo.getAvailableRAM();
            Console.WriteLine("********** End ***********");
            Console.WriteLine();
            Console.WriteLine("********** Hard Drive Information ***********");
            HardDriveInfo.GetHardDriveInfo();
            Console.WriteLine("********** End ***********");
            Console.WriteLine();
            Console.WriteLine("********** MS .net framwork Information ***********");
            DotNetFrameWorkInfo.GetDotNetVersion();
            DotNetFrameWorkInfo.GetVersionFromEnvironment();
            Console.WriteLine("********** End ***********");
            Console.WriteLine();
            Console.WriteLine("********** User Security Information ***********");
            UserSecurity.Administrator();
            UserSecurity.isCurrentUserLocalUser();
            Console.WriteLine("********** End ***********");
            Console.ReadLine();

        }
    }
}
