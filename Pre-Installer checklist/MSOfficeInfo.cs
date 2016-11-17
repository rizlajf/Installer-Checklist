using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pre_Installer_checklist
{
    static public class MSOfficeInfo
    {
        //Global variables
        static private int versionValue;

        static public string _Office_version
        {
            get
            {
                return Determine_OfficeVersion();
            }
        }

        static public string _Office_version_Bitness
        {
            get
            {
                return Determine_OfficeVersionBitness(versionValue);
            }
        }

        static private string Determine_OfficeVersion()
        {
            string strEVersionSubKey = "\\Excel.Application\\CurVer"; //HKEY_CLASSES_ROOT/Excel.Application/Curver

            string strValue = null; //Value Present In Above Key
            string strVersion = null; //Determines Excel Version

            RegistryKey rkVersion = null; //Registry Key To Determine Excel Version

            rkVersion = Registry.ClassesRoot.OpenSubKey(strEVersionSubKey, false); //Open Registry Key

            if ((rkVersion != null)) //If Key Exists
            {
                strValue = (string)rkVersion.GetValue(string.Empty); //Get Value

                strValue = strValue.Substring(strValue.LastIndexOf(".") + 1); //Store Value
                versionValue = Convert.ToInt32(strValue);
                switch (strValue) //Determine Version
                {
                    case "7":
                        strVersion = "95";
                        break;

                    case "8":
                        strVersion = "97";
                        break;

                    case "9":
                        strVersion = "2000";
                        break;

                    case "10":
                        strVersion = "2002";
                        break;

                    case "11":
                        strVersion = "2003";
                        break;

                    case "12":
                        strVersion = "2007";
                        break;

                    case "14":
                        strVersion = "2010";
                        break;

                    case "15":
                        strVersion = "2013";
                        break;

                }
            }

            return strVersion;
        }

        static private string Determine_OfficeVersionBitness(int versionValue)
        {
            string bitness = string.Empty;

            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(String.Format(@"SOFTWARE\Microsoft\Office\{0}.0\Outlook", versionValue));

            //if (registryKey == null)
            //    registryKey = Registry.LocalMachine.OpenSubKey(
            //        registryKeyPath.Insert("SOFTWARE".Length, "\\Wow6432Node"));

            if (registryKey != null)
                bitness = registryKey.GetValue("Bitness").ToString();

            return bitness;
        }
    }
}
