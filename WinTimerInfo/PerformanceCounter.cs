using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Threading;
using WinTimerInfo.Annotations;

namespace WinTimerInfo
{
    /// <summary>
    /// A timer class based around the QueryPerformanceCounter call
    /// </summary>
    public class PerformanceCounter : WinTimer
    {
        [DllImport("KERNEL32")]
        private static extern bool QueryPerformanceCounter(
            out long lpPerformanceCount);

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long lpFrequency);

        public PerformanceCounter(DispatcherTimer timer) : base(timer)
        {
        }

        protected override long ReadCount()
        {
            long count;
            QueryPerformanceCounter(out count);
            return count;
        }

        protected override void InitialiseFrequency()
        {
            long freq;
            QueryPerformanceFrequency(out freq);
            Frequency = freq;
        }
    }
}