using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace C_Sharp_newTech
{
    class Program
    {
        static void Main(string[] args)
        {
            string existingFile = @"D:\_Test\TestLink\orignalFile.txt";
            string NewFile = @"D:\_Test\TestLink\HardLinkTo_OrignalFile.txt";

            CreateHardLink(NewFile, existingFile, IntPtr.Zero); //(Hard Link: Can't be folder, Can't cross drive.
            //Junctions (Soft Link)
        }


        [DllImport("Kernel32.dll", CharSet = CharSet.Unicode)]
        static extern bool CreateHardLink(
                                          string lpFileName,
                                          string lpExistingFileName,
                                          IntPtr lpSecurityAttributes
                                          );
    }


}
