using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.ServiceProcess;
using System.Data.Sql;
using System.Data;

namespace Pre_Installer_checklist
{
    static class MSSqlInfo
    {
        static public string StatusOfMSSqlInstallation
        {
            get
            {
                return GetTheStatusOfMSSql();
            }
        }

        static private string GetTheStatusOfMSSql()
        {
            string _StatusOfMSSql = string.Empty;

            RegistryKey RK = Registry.CurrentUser.OpenSubKey("HKEY_LOCAL_MACHINE\\SOFTWARE\\MICROSOFT\\Microsoft SQL Server");
            if (RK != null)
            {
                _StatusOfMSSql = "Installed";
            }
            else
            {
                _StatusOfMSSql = "Not Installed";
            }


            return _StatusOfMSSql;
        }

        //public static void GetTheComponentsOfSQL()
        //{
        //    try
        //    {
        //        ManagementObjectSearcher searcher =
        //            new ManagementObjectSearcher("root\\CIMV2",
        //            "SELECT * FROM Win32_Product WHERE Name LIKE '%SQL%'");

        //        var objs = searcher.Get();

        //        if(objs.Count >0)
        //        {
        //            foreach (ManagementObject queryObj in objs)
        //            {
        //                Console.WriteLine("-----------------------------------");
        //                Console.WriteLine("Win32_Product instance");
        //                Console.WriteLine("-----------------------------------");
        //                Console.WriteLine("InstallDate: {0}", queryObj["InstallDate"]);
        //                Console.WriteLine("InstallLocation: {0}", queryObj["InstallLocation"]);
        //                Console.WriteLine("Name: {0}", queryObj["Name"]);
        //                Console.WriteLine("SKUNumber: {0}", queryObj["SKUNumber"]);
        //                Console.WriteLine("Vendor: {0}", queryObj["Vendor"]);
        //                Console.WriteLine("Version: {0}", queryObj["Version"]);
        //            }
        //        }
        //        else
        //            Console.WriteLine("There are no components to show");

        //    }
        //    catch (ManagementException e)
        //    {
        //        Console.WriteLine("An error occurred while querying for WMI data: " + e.Message);
        //    }
        //}

        public static void GetSqlServerInstances()
        {
            string servicename = "MSSQL";
            string servicename2 = "SQLAgent";
            string servicename3 = "SQL Server";
            string servicename4 = "msftesql";

            string serviceoutput = string.Empty;
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController service in services)
            {
                if (service == null)
                    continue;
                if (service.ServiceName.Contains(servicename) || service.ServiceName.Contains(servicename2) || service.ServiceName.Contains(servicename3) || service.ServiceName.Contains(servicename4))
                {
                    serviceoutput = serviceoutput + System.Environment.NewLine + "Service Name = " + service.ServiceName + System.Environment.NewLine + "Display Name = " + service.DisplayName + System.Environment.NewLine + "Status = " + service.Status + System.Environment.NewLine;
                }
            }

            if (serviceoutput == "")
            {
                serviceoutput += "There are no SQL Server instances present on this machine!" + System.Environment.NewLine;
            }
            else
                Console.WriteLine(serviceoutput);
        }

        public static void GetSqlInstances()
        {
            RegistryView registryView = Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32;
            using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView))
            {
                RegistryKey instanceKey = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL", false);
                if (instanceKey != null)
                {
                    foreach (var instanceName in instanceKey.GetValueNames())
                    {
                        Console.WriteLine(Environment.MachineName + @"\" + instanceName);
                    }
                }
                else
                {
                    RegistryKey instanceKey2 = hklm.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Microsoft SQL Server\Instance Names\SQL", false);
                    if (instanceKey2 != null)
                    {
                        foreach (var instanceName in instanceKey.GetValueNames())
                        {
                            Console.WriteLine(Environment.MachineName + @"\" + instanceName);
                        }
                    }
                }
            }
        }

        //list all SQL Servers on the network
        public static void GetTheVersionsOfSqlServer()
        {
            SqlDataSourceEnumerator sqldatasourceenumerator1 = SqlDataSourceEnumerator.Instance;
            DataTable datatable1 = sqldatasourceenumerator1.GetDataSources();
            foreach (DataRow row in datatable1.Rows)
            {
                Console.WriteLine("****************************************");
                //Server Name: Name of the server.
                Console.WriteLine("Server Name:" + row["ServerName"]);
                //Instance Name: Name of the SQL Server instance. If its a default instance then this is DBNull.
                Console.WriteLine("Instance Name:" + row["InstanceName"]);
                //Is Clustered: returns Yes, if Server is clustered, else No.
                Console.WriteLine("Is Clustered:" + row["IsClustered"]);
                //Version: returns the full version number of the server listed.
                Console.WriteLine("Version:" + row["Version"]);
                Console.WriteLine("****************************************");
            }
        }
    }
}
