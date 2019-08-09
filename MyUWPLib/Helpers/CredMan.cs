using System;
using System.Text;

using System.Runtime.InteropServices;

namespace MyUWPLib.Helpers
{
    public static class CredMan
    {
        enum CRED_TYPE : uint
        {
            CRED_TYPE_GENERIC = 1,
            CRED_TYPE_DOMAIN_PASSWORD = 2,
            CRED_TYPE_DOMAIN_CERTIFICATE = 3,
            CRED_TYPE_DOMAIN_VISIBLE_PASSWORD = 4
        }

        enum CRED_PERSIST : uint
        {
            CRED_PERSIST_SESSION = 1,
            CRED_PERSIST_LOCAL_MACHINE = 2,

            CRED_PERSIST_ENTERPRISE = 3
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct Credential
        {
            public UInt32 flags;
            public UInt32 type;
            public string targetName;
            public string comment;
            //public System.Runtime.InteropServices.FILETIME lastWritten; // .NET 1.1
            public System.Runtime.InteropServices.ComTypes.FILETIME lastWritten; // .NET 2.0
            public UInt32 credentialBlobSize;
            public IntPtr credentialBlob;
            public UInt32 persist;
            public UInt32 attributeCount;
            public IntPtr credAttribute;
            public string targetAlias;
            public string userName;
        }



        [DllImport("Advapi32.dll", SetLastError = true, EntryPoint = "CredWriteW", CharSet = CharSet.Unicode)]
        static extern bool CredWrite([In] ref Credential userCredential, [In] UInt32 flags);

        static public void AddDomainUserCredential(string target, string userName, string password)
        {
            Credential userCredential = new Credential();

            userCredential.targetName = target;
            userCredential.type = (UInt32)CRED_TYPE.CRED_TYPE_GENERIC;
            userCredential.userName = userName;
            userCredential.attributeCount = 0;
            userCredential.persist = (UInt32)CRED_PERSIST.CRED_PERSIST_LOCAL_MACHINE;
            byte[] bpassword = Encoding.Unicode.GetBytes(password);
            userCredential.credentialBlobSize = (UInt32)bpassword.Length;
            userCredential.credentialBlob = Marshal.StringToCoTaskMemUni(password);
            if (!CredWrite(ref userCredential, 0))
            {
                throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        static public void PCredWrite()
        {
            AddDomainUserCredential("r0s/account", "josephUserName", "josephPassword");
        }
    }
}
