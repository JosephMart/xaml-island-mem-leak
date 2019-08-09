using System;

using System.IO;
using System.Runtime.InteropServices;

namespace MyUWPLib.Helpers
{
    public static class File
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess,
       uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition,
       uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        public const short FILE_ATTRIBUTE_NORMAL = 0x80;
        public const short INVALID_HANDLE_VALUE = -1;
        public const uint GENERIC_READ = 0x80000000;
        public const uint GENERIC_WRITE = 0x40000000;
        public const uint CREATE_NEW = 1;
        public const uint CREATE_ALWAYS = 2;
        public const uint OPEN_EXISTING = 3;

        static public void PCreateFile(String path, String text)
        {
            IntPtr ptr = CreateFile(path, GENERIC_WRITE, 0,
            IntPtr.Zero, CREATE_NEW, 0, IntPtr.Zero);

            if (ptr.ToInt32() == -1)
            {
                /* ask the framework to marshall the win32 error code to an exception */
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            }
            else
            {
                //This is the cut command on Star TSP 600 Printer
                FileStream lpt = new FileStream(ptr, FileAccess.ReadWrite);
                Byte[] Buff = new Byte[1024];
                //Check to see if your printer support ASCII encoding or Unicode.
                //If unicode is supported, use the following:
                //Buff = System.Text.Encoding.Unicode.GetBytes(Temp);
                Buff = System.Text.Encoding.ASCII.GetBytes(text);
                lpt.Write(Buff, 0, Buff.Length);
                lpt.Close();
            }
        }

        [DllImport("Comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetOpenFileName([In, Out] OpenFileName ofn);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class OpenFileName
        {
            public int structSize = 0;
            public IntPtr dlgOwner = IntPtr.Zero;
            public IntPtr instance = IntPtr.Zero;

            public String filter = null;
            public String customFilter = null;
            public int maxCustFilter = 0;
            public int filterIndex = 0;

            public String file = null;
            public int maxFile = 0;

            public String fileTitle = null;
            public int maxFileTitle = 0;

            public String initialDir = null;

            public String title = null;

            public int flags = 0;
            public short fileOffset = 0;
            public short fileExtension = 0;

            public String defExt = null;

            public IntPtr custData = IntPtr.Zero;
            public IntPtr hook = IntPtr.Zero;

            public String templateName = null;

            public IntPtr reservedPtr = IntPtr.Zero;
            public int reservedInt = 0;
            public int flagsEx = 0;
        }


        static public String OpenFileDialog()
        {
            OpenFileName ofn = new OpenFileName();

            ofn.structSize = Marshal.SizeOf(ofn);

            ofn.filter = "Text files\0*.txt\0";

            ofn.file = new String(new char[256]);
            ofn.maxFile = ofn.file.Length;

            ofn.fileTitle = new String(new char[64]);
            ofn.maxFileTitle = ofn.fileTitle.Length;

            ofn.initialDir = "C:\\";
            ofn.title = "Open file called using platform invoke...";
            ofn.defExt = "txt";

            GetOpenFileName(ofn);
            return ofn.file;
        }
    }
}
