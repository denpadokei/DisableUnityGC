using System.Runtime.InteropServices;

namespace DisableUnityGC.Utilites
{
    public class NativeMethods
    {
        [DllImport("Libs/MemoryViewer.dll")]
        public extern static ulong GetWorkingSet();

        [DllImport("Libs/MemoryViewer.dll")]
        public extern static ulong GetCommitSize();
    }
}
