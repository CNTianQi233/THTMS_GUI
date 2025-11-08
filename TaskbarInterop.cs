using System;
using System.Runtime.InteropServices;

namespace THTMS_GUI
{
    /// <summary>
    /// ITaskbarList3 互操作与轻量包装。
    /// </summary>
    internal static class TaskbarInterop
    {
        // TBPFLAG（任务栏进度状态）
        internal enum TBPFLAG
        {
            TBPF_NOPROGRESS = 0,
            TBPF_INDETERMINATE = 0x1,
            TBPF_NORMAL = 0x2,
            TBPF_ERROR = 0x4,
            TBPF_PAUSED = 0x8
        }

        // COM 类：TaskbarList
        [ComImport]
        [Guid("56FDF344-FD6D-11d0-958A-006097C9A090")]
        private class CTaskbarList { }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("56FDF342-FD6D-11d0-958A-006097C9A090")]
        private interface ITaskbarList
        {
            [PreserveSig] void HrInit();
            [PreserveSig] void AddTab(IntPtr hwnd);
            [PreserveSig] void DeleteTab(IntPtr hwnd);
            [PreserveSig] void ActivateTab(IntPtr hwnd);
            [PreserveSig] void SetActiveAlt(IntPtr hwnd);
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("602D4995-B13A-429b-A66E-1935E44F4317")]
        private interface ITaskbarList2 : ITaskbarList
        {
            // 重新声明基类方法以确保 vtable 顺序
            [PreserveSig] new void HrInit();
            [PreserveSig] new void AddTab(IntPtr hwnd);
            [PreserveSig] new void DeleteTab(IntPtr hwnd);
            [PreserveSig] new void ActivateTab(IntPtr hwnd);
            [PreserveSig] new void SetActiveAlt(IntPtr hwnd);

            [PreserveSig] void MarkFullscreenWindow(IntPtr hwnd, [MarshalAs(UnmanagedType.Bool)] bool fFullscreen);
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("EA1AFB91-9E28-4B86-90E9-9E9F8A5EEA84")]
        private interface ITaskbarList3 : ITaskbarList2
        {
            // 这里只声明前两个我们需要用到的方法；后续方法不调用可省略
            [PreserveSig] void SetProgressValue(IntPtr hwnd, ulong ullCompleted, ulong ullTotal);
            [PreserveSig] void SetProgressState(IntPtr hwnd, TBPFLAG tbpFlags);
        }

        /// <summary>
        /// 面向 Form 的易用包装。
        /// </summary>
        internal sealed class TaskbarProgress : IDisposable
        {
            private ITaskbarList3 _tb;
            private readonly IntPtr _hwnd;
            private readonly bool _supported;

            internal TaskbarProgress(IntPtr hwnd)
            {
                _hwnd = hwnd;
                _supported = Environment.OSVersion.Platform == PlatformID.Win32NT
                             && Environment.OSVersion.Version >= new Version(6, 1); // Win7+

                if (_supported)
                {
                    try
                    {
                        _tb = (ITaskbarList3)new CTaskbarList();
                        _tb.HrInit();
                    }
                    catch
                    {
                        _supported = false;
                        _tb = null;
                    }
                }
            }

            internal void SetState(TBPFLAG state)
            {
                if (_supported && _tb != null)
                {
                    try { _tb.SetProgressState(_hwnd, state); } catch { /* ignore */ }
                }
            }

            internal void SetValue(ulong current, ulong total)
            {
                if (_supported && _tb != null)
                {
                    try { _tb.SetProgressValue(_hwnd, current, total); } catch { /* ignore */ }
                }
            }

            internal void Clear() => SetState(TBPFLAG.TBPF_NOPROGRESS);

            public void Dispose()
            {
                try { Clear(); } catch { }
                _tb = null;
            }
        }
    }
}
