using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Collections;

namespace RegistrySample
{
    class RegistryHelp
    {
        private const String HKEY_CLASSES_ROOT = "HKEY_CLASSES_ROOT\\";
        private const String HKEY_CURRENT_USER = "HKEY_CURRENT_USER\\";
        private const String HKEY_LOCAL_MACHINE = "HKEY_LOCAL_MACHINE\\";
        private const String HKEY_USERS = "HKEY_USERS\\";
        private const String HKEY_CURRENT_CONFIG = "HKEY_CURRENT_CONFIG\\";


        static private RegistryKey GetKey(String RegPath)
        {
            RegistryKey RegKey = null;
            try
            {
                if (RegPath.StartsWith(HKEY_CLASSES_ROOT))
                {
                    RegKey = Registry.ClassesRoot.OpenSubKey(RegPath.Substring(HKEY_CLASSES_ROOT.Length));
                }
                else if (RegPath.StartsWith(HKEY_CURRENT_USER))
                {
                    RegKey = Registry.CurrentUser.OpenSubKey(RegPath.Substring(HKEY_CURRENT_USER.Length));
                }
                else if (RegPath.StartsWith(HKEY_LOCAL_MACHINE))
                {
                    RegKey = Registry.LocalMachine.OpenSubKey(RegPath.Substring(HKEY_LOCAL_MACHINE.Length));
                }
                else if (RegPath.StartsWith(HKEY_USERS))
                {
                    RegKey = Registry.Users.OpenSubKey(RegPath.Substring(HKEY_USERS.Length));
                }
                else if (RegPath.StartsWith(HKEY_CURRENT_CONFIG))
                {
                    RegKey = Registry.CurrentConfig.OpenSubKey(RegPath.Substring(HKEY_CURRENT_CONFIG.Length));
                }
            }
            catch (Exception e) { 
            
            }
            
            return RegKey;
        }

        static public String ReadRegistryKey(String RegPath, String Name)
        {
            RegistryKey RegKey = GetKey(RegPath);

            try 
            {
                String RegValue = (String)RegKey.GetValue(Name);
                return RegValue;
            }
            catch 
            {
                return null;
            }
        }

        static public void ReadRegistryKey(String RegPath, IDictionary dict)
        {
            RegistryKey RegKey = GetKey(RegPath);
            if (RegKey == null) return;

            foreach (String Name in RegKey.GetValueNames())
            {
                RegistryValueKind RegKeyKind = RegKey.GetValueKind(Name);

                switch (RegKeyKind)
                {
                    case RegistryValueKind.MultiString:
                        String[] values = (String[])RegKey.GetValue(Name);
                        if (Name.Length == 0)
                        {
                            dict.Add("(預設值)", String.Join(",", values));
                        }
                        else 
                        {
                            dict.Add(Name, String.Join(",", values));
                        }                        
                        break;

                    case RegistryValueKind.String:
                        if (Name.Length == 0)
                        {
                            dict.Add("(預設值)", (String)RegKey.GetValue(Name));
                        }
                        else
                        {
                            dict.Add(Name, (String)RegKey.GetValue(Name));
                        }
                        break;

               
                    
                
                    default:
                        if (Name.Length == 0)
                        {
                            dict.Add("(預設值)", RegKey.GetValue(Name).ToString());
                        }
                        else
                        {
                            dict.Add(Name, RegKey.GetValue(Name).ToString());
                        }
                        break;
                }
            }

            foreach (String SubName in RegKey.GetSubKeyNames())
            {
                IDictionary sub_dict = new Dictionary<String, Object>();
                dict.Add(SubName, sub_dict);
                ReadRegistryKey(RegPath + "\\" + SubName, sub_dict);
                
            }
           
        }
    }
}
