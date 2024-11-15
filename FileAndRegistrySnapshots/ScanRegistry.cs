using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace FileAndRegistrySnapshots
{
    public static class ScanRegistry
    {
        private static Dictionary<Type, int> ValueTypes = new Dictionary<Type, int>();
        private static int blankValues = 0;
        private static int errors = 0;

        public static void Start()
        {
            var data = new Dictionary<string, string>();

            var folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var files = Directory.GetFiles(folder, "aa.reg");

            var count = 0;
            using (StreamReader sr = File.OpenText(files[0]))
            {
                string s = String.Empty;
                string mainKey = null;
                string key = null;
                string value = null;
                while ((s = sr.ReadLine()) != null)
                {
                    s = s.Trim();
                    if (data.Count == 10) ;
                    if (string.IsNullOrEmpty(s))
                    {
                        if (!string.IsNullOrEmpty(mainKey))
                            data.Add(mainKey, value);
                        mainKey = null;
                        key = null;
                        value = null;
                    }
                    else
                    {
                        if (mainKey == null)
                        {
                            if (s.StartsWith("[") && s.EndsWith("]"))
                                mainKey = s.Substring(1, s.Length - 2);
                            else
                                mainKey = s;
                        }
                        else if (key == null)
                        {
                            var i1 = s.IndexOf('=');
                            key = s.Substring(0,i1).Trim();
                            value = s.Substring(i1 + 1).Trim();
                        }
                        else
                        {
                            if (value.EndsWith("\\"))
                            {
                                value = value.Substring(0, value.Length - 1);
                            }
                            else if (s.StartsWith("@") || s.StartsWith("\""))
                            {
                                SaveValue(mainKey, key, value, data);

                                var i1 = s.IndexOf('=');
                                key = s.Substring(0, i1).Trim();
                                value = s.Substring(i1 + 1).Trim();
                            }
                            else
                            {
                                throw new Exception("gfjytk");
                            }
                            value += s;
                        }
                    }
                }

                SaveValue(mainKey, key, value, data);
            }
        }

        private static void SaveValue(string mainKey, string key, string value, Dictionary<string, string> data)
        {
            string dataKey;
            if (key == "@") dataKey = mainKey + "\\";
            else if (key.StartsWith("\"") || key.EndsWith("\""))
                dataKey = mainKey + "\\" + key.Substring(1, key.Length - 2);
            else throw new Exception("AAAAAAA");

            data.Add(dataKey, value);
        }

        public static void Start2()
        {
            var sw = new Stopwatch();
            sw.Start();
            var registryKeys = new[]
            {
                Registry.ClassesRoot, Registry.CurrentUser, Registry.LocalMachine, Registry.Users,
                Registry.CurrentConfig/*, Registry.PerformanceData*/
            };

            foreach (var key in registryKeys)
            {
                OutputRegKey(key);
            }

            int count = blankValues;
            foreach (var kvp in ValueTypes)
            {
                count += kvp.Value;
                Debug.Print($"{kvp.Key}\t{kvp.Value:N0}");
            }

            Debug.Print($"EmptyKeys\t{blankValues:N0}");
            Debug.Print($"ErrorKeys\t{errors:N0}");
            Debug.Print($"TotalKeys\t{count:N0}");

            sw.Stop();
            Debug.Print($"\nDuration: {sw.ElapsedMilliseconds/1000:N0} seconds");
        }

        private static void ProcessValueNames(RegistryKey key)
        { //function to process the valueNames for a given key
            string[] valueNames = key.GetValueNames();
            if (valueNames.Length == 0)
            {
                blankValues++;
                return;
            }

            foreach (string valueName in valueNames)
            {
                object obj = key.GetValue(valueName);
                var type = obj.GetType();
                if (!ValueTypes.ContainsKey(type))
                    ValueTypes.Add(type, 0);
                ValueTypes[type]++;
                // Debug.Print($"{key.ToString()}\t{obj.ToString()}");
            }
        }

        public static void OutputRegKey(RegistryKey key)
        {
            if (key == null) return;

            try
            {
                ProcessValueNames(key);
            }
            catch (Exception e)
            {
            }

            try
            {
                var subKeyNames = key.GetSubKeyNames(); //means deeper folder
                foreach (string keyName in subKeyNames)
                {
                    using (RegistryKey key2 = key.OpenSubKey(keyName))
                        OutputRegKey(key2);
                }
            }
            catch (SecurityException)
            {
                errors++;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
