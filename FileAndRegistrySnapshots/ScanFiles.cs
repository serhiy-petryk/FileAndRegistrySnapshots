using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileAndRegistrySnapshots
{
    public static class ScanFiles
    {
        public static void Start()
        {
            var folders = Enum.GetValues(typeof(Environment.SpecialFolder));
            foreach (Environment.SpecialFolder folder in folders)
            {
                Debug.Print($"{folder}\t{Environment.GetFolderPath(folder)}");
            }

            var files = new List<string>();
            var tmpFolder = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            ProcessFolder(tmpFolder, files);
        }

        private static void ProcessFolder(string folder, List<string> files)
        {
            try
            {
                var tmpFiles = Directory.GetFiles(folder);
                files.AddRange(tmpFiles);
            }
            catch (UnauthorizedAccessException)
            {
            }
            catch (Exception ex)
            {
                throw ex;
            }

            var tmpFolders = Directory.GetDirectories(folder);
            foreach (var tmpFolder in tmpFolders)
            {
                try
                {
                    ProcessFolder(tmpFolder, files);
                }
                catch (UnauthorizedAccessException)
                {
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
