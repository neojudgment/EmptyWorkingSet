// •————————————————————————————————————————————————————————————————————————————————————————————————————————————•
// |                                                                                                            |
// |    The EmptyWorkingSet function takes a process handle. It removes as many pages as possible from          |
// |    all processes working set.                                                                              |
// |                                                                                                            |
// |    This operation is useful primarily for testing, tuning and replace Garbage Collector (GC) in real-time. |
// |                                                                                                            |
// |    https://msdn.microsoft.com/fr-fr/us-us/library/windows/desktop/ms687398(v=vs.85).aspx                   |
// |                                                                                                            |
// |    Pascal Hubert - Brussels, Belgium 2025. <mailto:pascal.hubert@outlook.com>                              |
// •————————————————————————————————————————————————————————————————————————————————————————————————————————————•


using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace EmptyWorkingSet
{
    internal class Program
    {
        [DllImport("psapi.dll")]
        private extern static bool EmptyWorkingSet(IntPtr hProcess);

        static void Main()
        {
            while (true)
            {
                MemoryClean();
                System.Threading.Thread.Sleep(30000); // Sleep for 30 seconds
            }

        }

        private static void MemoryClean()
        {
            foreach (Process proc in Process.GetProcesses())
            {
                try
                {
                    // Removes as many pages as possible from the working set of the specified process above.
                    bool successful = EmptyWorkingSet(proc.Handle);
                    if (!successful)
                    {
                        Console.WriteLine($"{DateTime.Now} EmptyWorkingSet failed for process {proc.ProcessName} (ID: {proc.Id})");
                    }
                }
                catch (Exception ex)
                {
                    // Some processes are not accessible (rights, etc.)
                    Console.WriteLine($"{DateTime.Now} Exception for process {proc.ProcessName} (ID: {proc.Id}): {ex.Message}");
                }
            }
        }
    }
}
