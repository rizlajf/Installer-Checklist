using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Pre_Installer_checklist
{
    static class RAMInfo
    {
        public static void GetTheMemoryInfo()
        {
            // Determine the best available approximation of the number 
            // of bytes currently allocated in managed memory.
            Console.WriteLine("Total Memory: {0}", GC.GetTotalMemory(false));
        }

        //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        //private class MEMORYSTATUSEX
        //{
        //    public uint dwLength;
        //    public uint dwMemoryLoad;
        //    public ulong ullTotalPhys;
        //    public ulong ullAvailPhys;
        //    public ulong ullTotalPageFile;
        //    public ulong ullAvailPageFile;
        //    public ulong ullTotalVirtual;
        //    public ulong ullAvailVirtual;
        //    public ulong ullAvailExtendedVirtual;
        //    public MEMORYSTATUSEX()
        //    {
        //        this.dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX));
        //    }
        //}


        //[return: MarshalAs(UnmanagedType.Bool)]
        //[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //static extern bool GlobalMemoryStatusEx([In, Out] MEMORYSTATUSEX lpBuffer);

        //public static void GetAvailableRam()
        //{
        //    ulong installedMemory;
        //    MEMORYSTATUSEX memStatus = new MEMORYSTATUSEX();
        //    if (GlobalMemoryStatusEx(memStatus))
        //    {
        //        installedMemory = memStatus.ullTotalPhys;
        //        Console.WriteLine((installedMemory / 1024 / 1024 / 1024) + " GB of RAM Available.");
        //    }
        //}


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal class MEMORYSTATUSEX
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public ulong ullTotalPhys;
            public ulong ullAvailPhys;
            public ulong ullTotalPageFile;
            public ulong ullAvailPageFile;
            public ulong ullTotalVirtual;
            public ulong ullAvailVirtual;
            public ulong ullAvailExtendedVirtual;

            public MEMORYSTATUSEX()
            {
                this.dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX));
            }
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern Boolean GlobalMemoryStatusEx([In, Out] MEMORYSTATUSEX lpBuffer);

        public static String GetTotalRam
        {
            get
            {
                ulong installedMemory = 0;
                MEMORYSTATUSEX memStatus = new MEMORYSTATUSEX();
                if (GlobalMemoryStatusEx(memStatus))
                {
                    installedMemory = memStatus.ullTotalPhys;
                }
                return ConvertBytes(installedMemory);
            }
        }

        private static string ConvertBytes(ulong installedMemory)
        {
            return (installedMemory / 1024 / 1024 / 1024).ToString("##.#GB");
        }

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetPhysicallyInstalledSystemMemory(out long TotalMemoryInKilobytes);

        // get the amount of installed memory
        public static void GetInstalledMemory()
        {
            long memKb;
            GetPhysicallyInstalledSystemMemory(out memKb);
            Console.WriteLine((memKb / 1024 / 1024) + " GB of RAM installed.");
        }

        public static void getAvailableRAM()
        {
            ComputerInfo CI = new ComputerInfo();
            long mem = long.Parse(CI.TotalPhysicalMemory.ToString());
            Console.WriteLine((mem / 1024 / 1024/1024) + " GB of RAM Available.");
            //lbl_Avilable_Memory.Text = (mem / (1024 * 1024) + " MB").ToString();
        }
    }
}
