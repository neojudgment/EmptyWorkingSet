// •————————————————————————————————————————————————————————————————————————————————————————————————————————————•
// |                                                                                                            |
// |    The EmptyWorkingSet function takes a process handle. It removes as many pages as possible from          |
// |    the process working set.                                                                                |
// |                                                                                                            |
// |    This operation is useful primarily for testing, tuning and replace Garbage Collector (GC) in real-time. |
// |                                                                                                            |
// |    https://msdn.microsoft.com/fr-fr/us-us/library/windows/desktop/ms687398(v=vs.85).aspx                   |
// |                                                                                                            |
// |    Copyright © Pascal Hubert - Brussels, Belgium 2017. <mailto:pascal.hubert@outlook.com>                  |
// •————————————————————————————————————————————————————————————————————————————————————————————————————————————•

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;

namespace psapi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///
    public partial class MainWindow : Window
    {
        [DllImport("psapi.dll")]
        private extern static bool EmptyWorkingSet(IntPtr hProcess);

        public MainWindow()
        {
            InitializeComponent();
            MemoryClean();
        }

        private static void MemoryClean()
        {
            // Takes process handle.
            Process pProcess = Process.GetCurrentProcess();

            // Removes as many pages as possible from the working set of the specified process above.
            bool Successful = EmptyWorkingSet(pProcess.Handle);

            if (!Successful)
            {
                // EmptyWorkingSet failed...(In some cases!)
                Console.WriteLine(DateTime.Now + " " + "EmptyWorkingSet failed!");
            }
            Console.WriteLine(DateTime.Now + " " + "Memory allocated: " + Environment.WorkingSet.ToString() + " Bytes");
        }
    }
}