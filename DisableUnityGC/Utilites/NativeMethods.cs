using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DisableUnityGC.Utilites
{
    public class NativeMethods
    {
        [DllImport("Libs/MemoryViewer.dll")]
        public extern static long GetWorkingSet();

        [DllImport("Libs/MemoryViewer.dll")]
        public extern static long GetCommitSize();
    }
}
