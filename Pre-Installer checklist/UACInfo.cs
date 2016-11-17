using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pre_Installer_checklist
{
    static class UACInfo
    {
        static public string StatusOfUAC
        {
            get
            {
                return GetTheStatusOfUAC();
            }
        }

        static private string GetTheStatusOfUAC()
        {
            string ValueOfEnableLUA = string.Empty;
            string _StatusOfUAC = string.Empty;
            
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", true);
           
            if (registryKey != null)
                ValueOfEnableLUA = registryKey.GetValue("EnableLUA").ToString();
            
            _StatusOfUAC = (string.IsNullOrEmpty(ValueOfEnableLUA) || ValueOfEnableLUA == "0") ? "UAC is not enabled" : "UAC is enabled";


            return _StatusOfUAC;
        }
    }
}
