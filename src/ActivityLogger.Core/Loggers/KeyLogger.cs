using System;
using System.Diagnostics;
using AL.Core.Models;
using AL.Core.Utilities;
using LowLevelKeyboardProc = AL.Core.Utilities.NativeMethods.LowLevelKeyboardProc;

namespace AL.Core.Loggers
{
    public class KeyLogger : Logger<KeyReport>
    {
        private static KeyLogger _instance;
        public static KeyLogger Instance()
        {
            return _instance ?? (_instance = new KeyLogger());
        }

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;

        private static readonly LowLevelKeyboardProc Proc = HookCallback;

        private static IntPtr _hookId = IntPtr.Zero;
        
        private int _keyStrokes;

        private KeyLogger()
        {
            _hookId = SetHook(Proc);
        }

        public sealed override void Dispose()
        {
            NativeMethods.UnhookWindowsHookEx(_hookId);
        }
        
        public override void Log()
        {
            var keyReport = new KeyReport
            {
                TotalKeyStrokes = ++_keyStrokes,
                KeyStrokes = 1,
                LatestActivity = DateTime.Now
            };

            Observer.OnNext(keyReport);
        }
        
        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (var curProcess = Process.GetCurrentProcess())
            {
                using (var curModule = curProcess.MainModule)
                {
                    return NativeMethods.SetWindowsHookEx(WH_KEYBOARD_LL, proc, NativeMethods.GetModuleHandle(curModule.ModuleName), 0);
                }
            }
        }
        
        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYUP)
            {
                Instance().Log();
            }

            return NativeMethods.CallNextHookEx(_hookId, nCode, wParam, lParam);
        }
    }
}
