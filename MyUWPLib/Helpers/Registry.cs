﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MyUWPLib.Helpers
{
    public static class Registry
    {
        [DllImport("Advapi32.dll", EntryPoint = "RegOpenKeyExW", CharSet = CharSet.Unicode)]
        static extern int RegOpenKeyEx(IntPtr hKey, [In] string lpSubKey, int ulOptions, int samDesired, out IntPtr phkResult);
        [DllImport("Advapi32.dll", EntryPoint = "RegQueryValueExW", CharSet = CharSet.Unicode)]
        static extern int RegQueryValueEx(IntPtr hKey, [In] string lpValueName, IntPtr lpReserved, out int lpType, [Out] byte[] lpData, ref int lpcbData);
        [DllImport("Advapi32.dll")]
        static extern int RegCloseKey(IntPtr hKey);

        static public readonly IntPtr HKEY_CLASSES_ROOT = new IntPtr(-2147483648);
        static public readonly IntPtr HKEY_CURRENT_USER = new IntPtr(-2147483647);
        static public readonly IntPtr HKEY_LOCAL_MACHINE = new IntPtr(-2147483646);
        static public readonly IntPtr HKEY_USERS = new IntPtr(-2147483645);
        static public readonly IntPtr HKEY_PERFORMANCE_DATA = new IntPtr(-2147483644);
        static public readonly IntPtr HKEY_CURRENT_CONFIG = new IntPtr(-2147483643);
        static public readonly IntPtr HKEY_DYN_DATA = new IntPtr(-2147483642);

        public const int KEY_READ = 0x20019;
        public const int KEY_WRITE = 0x20006;
        public const int KEY_QUERY_VALUE = 0x0001;
        public const int KEY_SET_VALUE = 0x0002;
        public const int KEY_WOW64_64KEY = 0x0100;
        public const int KEY_WOW64_32KEY = 0x0200;

        public const int REG_NONE = 0;
        public const int REG_SZ = 1;
        public const int REG_EXPAND_SZ = 2;
        public const int REG_BINARY = 3;
        public const int REG_DWORD = 4;
        public const int REG_DWORD_BIG_ENDIAN = 5;
        public const int REG_LINK = 6;
        public const int REG_MULTI_SZ = 7;
        public const int REG_RESOURCE_LIST = 8;
        public const int REG_FULL_RESOURCE_DESCRIPTOR = 9;
        public const int REG_RESOURCE_REQUIREMENTS_LIST = 10;
        public const int REG_QWORD = 11;

        static object RegQueryValue(IntPtr key, string value)
        {
            return RegQueryValue(key, value, null);
        }

        static object RegQueryValue(IntPtr key, string value, object defaultValue)
        {
            int error, type = 0, dataLength = 0xfde8;
            int returnLength = dataLength;
            byte[] data = new byte[dataLength];
            while ((error = RegQueryValueEx(key, value, IntPtr.Zero, out type, data, ref returnLength)) == 0xea)
            {
                dataLength *= 2;
                returnLength = dataLength;
                data = new byte[dataLength];
            }
            if (error == 2)
                return defaultValue; // value doesn't exist
            if (error != 0)
                return "error";

            switch (type)
            {
                case REG_NONE:
                case REG_BINARY:
                    return data;
                case REG_DWORD:
                    return (((data[0] | (data[1] << 8)) | (data[2] << 16)) | (data[3] << 24));
                case REG_DWORD_BIG_ENDIAN:
                    return (((data[3] | (data[2] << 8)) | (data[1] << 16)) | (data[0] << 24));
                case REG_QWORD:
                    {
                        uint numLow = (uint)(((data[0] | (data[1] << 8)) | (data[2] << 16)) | (data[3] << 24));
                        uint numHigh = (uint)(((data[4] | (data[5] << 8)) | (data[6] << 16)) | (data[7] << 24));
                        return (long)(((ulong)numHigh << 32) | (ulong)numLow);
                    }
                case REG_SZ:
                    return Encoding.Unicode.GetString(data, 0, returnLength);
                case REG_EXPAND_SZ:
                    return Environment.ExpandEnvironmentVariables(Encoding.Unicode.GetString(data, 0, returnLength));
                case REG_MULTI_SZ:
                    {
                        var strings = new List<string>();
                        string packed = Encoding.Unicode.GetString(data, 0, returnLength);
                        int start = 0;
                        int end = packed.IndexOf('\0', start);
                        while (end > start)
                        {
                            strings.Add(packed.Substring(start, end - start));
                            start = end + 1;
                            end = packed.IndexOf('\0', start);
                        }
                        return strings.ToArray();
                    }
                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Read from HKEY_LOCAL_MACHINE\<path>\<keyVal>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="keyVal"></param>
        /// <returns></returns>
        static public object PReadRegistry(string path, string keyVal)
        {
            IntPtr key;
            int error;
            if ((error = RegOpenKeyEx(HKEY_LOCAL_MACHINE, path, 0, KEY_READ, out key)) != 0)
                return null;
            try
            {
                return RegQueryValue(key, keyVal);
            }
            finally
            {
                RegCloseKey(key);
            }
        }
    }
}
