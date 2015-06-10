using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileIOTest
{
    class Program
    {
        public static void DeleteDirectoryForcely(string sourceDirName)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            if (!dir.Exists)
            {
                return;
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            foreach (var dirItem in dirs)
            {
                DeleteDirectoryForcely(dirItem.FullName);
            }

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                File.SetAttributes(file.FullName, FileAttributes.Normal);
                File.Delete(file.FullName);
            }

            dir.Delete();
        }
        static void Main(string[] args)
        {
            //Delete ReadOnly file.
            string filePath = Path.GetFullPath(@"TestFiles\ReadOnlyFile.txt");
            File.SetAttributes(filePath, FileAttributes.ReadOnly);
            try
            {
                File.Delete(filePath);

            }
            catch
            {
                string Dir = Path.GetDirectoryName(filePath);
                DeleteDirectoryForcely(Dir);
            }

            try
            {
                File.SetAttributes(filePath, FileAttributes.Normal);
            }
            catch
            {

            }

            try
            {
                FileInfo toDelFile = new FileInfo(filePath);
                toDelFile.IsReadOnly = false;
                toDelFile.Delete();
            }
            catch
            {

            }
        }
    }
}
