using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace MyUWPLib.Helpers
{
    public static class MyDLL
    {

        [DllImport("MyDLL.dll")]
        public static extern int fib(int n);

        [DllImport("MyDLL.dll")]
        public static extern int test();
    }
}
