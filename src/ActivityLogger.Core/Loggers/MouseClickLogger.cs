using System;
using System.Diagnostics;
using AL.Core.Models;
using AL.Core.Utilities;
using LowLevelMouseProc = AL.Core.Utilities.NativeMethods.LowLevelMouseProc;

namespace AL.Core.Loggers
{
    public class MouseClickLogger : Logger<MouseClickReport>
    {
        private static MouseClickLogger _instance;
        public static MouseClickLogger Instance()
        {
            return _instance ?? (_instance = new MouseClickLogger());
        }

        private const int WH_MOUSE_LL = 14;
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_RBUTTONDOWN = 0x0204;
        private const int WM_MOUSEWHEEL = 0x020A;

        private readonly LowLevelMouseProc _proc;

        private static IntPtr _hookId = IntPtr.Zero;
        
        private int _clicks;

        private MouseClickLogger()
        {
            _proc = HookCallback;

            // Don't listen to mouse clicks if debugging. It's a pain in the ass.
            if (!Debugger.IsAttached)
                _hookId = SetHook(_proc);
        }

        public sealed override void Dispose()
        {
            NativeMethods.UnhookWindowsHookEx(_hookId);
        }

        public override void Log()
        {
            var mouseClickReport = new MouseClickReport
            {
                TotalClicks = ++_clicks,
                Clicks = 1,
                LatestActivity = DateTime.Now
            };

            Observer.OnNext(mouseClickReport);
        }

        private static IntPtr SetHook(LowLevelMouseProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            {
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    return NativeMethods.SetWindowsHookEx(WH_MOUSE_LL, proc,
                        NativeMethods.GetModuleHandle(curModule.ModuleName), 0);
                }
            }
        }

        public static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                if ((IntPtr)WM_LBUTTONDOWN == wParam)
                {
                    Instance().Log();
                }
            }

            return NativeMethods.CallNextHookEx(_hookId, nCode, wParam, lParam);
        }
    }
}
