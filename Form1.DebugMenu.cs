using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace THTMS_GUI
{
    public partial class TaskbarDebugForm : Form
    {
        private TaskbarInteropPrivate.TaskbarController? _controller;
        private Label lblStatus;
        private TrackBar trackBar;
        private Label lblProgress;
        private Button btnNormal;
        private Button btnPaused;
        private Button btnError;
        private Button btnClear;
        private TextBox txtLog;

        public TaskbarDebugForm()
        {
            InitializeComponent();
            InitializeControls();

            // 窗体加载后初始化 Taskbar
            this.Load += TaskbarDebugForm_Load;
            this.FormClosing += TaskbarDebugForm_FormClosing;
        }

        private void InitializeControls()
        {
            this.Text = "Taskbar Progress 调试工具";
            this.Size = new System.Drawing.Size(600, 500);
            this.ShowInTaskbar = true;
            this.StartPosition = FormStartPosition.CenterScreen;

            // 状态标签
            lblStatus = new Label
            {
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(550, 60),
                Font = new System.Drawing.Font("Consolas", 9F),
                Text = "正在初始化..."
            };

            // 进度条
            trackBar = new TrackBar
            {
                Location = new System.Drawing.Point(20, 100),
                Size = new System.Drawing.Size(550, 45),
                Minimum = 0,
                Maximum = 100,
                Value = 0,
                TickFrequency = 10
            };
            trackBar.ValueChanged += TrackBar_ValueChanged;

            // 进度标签
            lblProgress = new Label
            {
                Location = new System.Drawing.Point(20, 150),
                Size = new System.Drawing.Size(550, 20),
                Text = "进度: 0%"
            };

            // 按钮组
            btnNormal = new Button
            {
                Location = new System.Drawing.Point(20, 180),
                Size = new System.Drawing.Size(120, 30),
                Text = "正常模式"
            };
            btnNormal.Click += (s, e) => SetState(TaskbarInteropPrivate.TBPFLAG.TBPF_NORMAL);

            btnPaused = new Button
            {
                Location = new System.Drawing.Point(150, 180),
                Size = new System.Drawing.Size(120, 30),
                Text = "暂停模式"
            };
            btnPaused.Click += (s, e) => SetState(TaskbarInteropPrivate.TBPFLAG.TBPF_PAUSED);

            btnError = new Button
            {
                Location = new System.Drawing.Point(280, 180),
                Size = new System.Drawing.Size(120, 30),
                Text = "错误模式"
            };
            btnError.Click += (s, e) => SetState(TaskbarInteropPrivate.TBPFLAG.TBPF_ERROR);

            btnClear = new Button
            {
                Location = new System.Drawing.Point(410, 180),
                Size = new System.Drawing.Size(120, 30),
                Text = "清除进度"
            };
            btnClear.Click += (s, e) => ClearProgress();

            // 日志文本框
            txtLog = new TextBox
            {
                Location = new System.Drawing.Point(20, 220),
                Size = new System.Drawing.Size(550, 220),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true,
                Font = new System.Drawing.Font("Consolas", 8.5F)
            };

            // 添加控件
            this.Controls.Add(lblStatus);
            this.Controls.Add(trackBar);
            this.Controls.Add(lblProgress);
            this.Controls.Add(btnNormal);
            this.Controls.Add(btnPaused);
            this.Controls.Add(btnError);
            this.Controls.Add(btnClear);
            this.Controls.Add(txtLog);
        }

        private void TaskbarDebugForm_Load(object? sender, EventArgs e)
        {
            LogMessage("========== 开始初始化 ==========");

            // 诊断信息
            var hwnd = this.Handle;
            var apartment = Thread.CurrentThread.GetApartmentState();
            var bitness = Environment.Is64BitProcess ? "x64" : "x86";
            var osVersion = Environment.OSVersion.Version;

            LogMessage($"窗体句柄: 0x{hwnd:X}");
            LogMessage($"ShowInTaskbar: {this.ShowInTaskbar}");
            LogMessage($"窗体可见: {this.Visible}");
            LogMessage($"线程单元: {apartment}");
            LogMessage($"进程位数: {bitness}");
            LogMessage($"操作系统: Windows {osVersion}");
            LogMessage("");

            try
            {
                _controller = new TaskbarInteropPrivate.TaskbarController(hwnd);

                lblStatus.Text = $"✓ 状态：已连接 (Normal)\n" +
                               $"线程单元：{apartment} | 进程位数：{bitness}\n" +
                               $"句柄：0x{hwnd:X}";
                lblStatus.ForeColor = System.Drawing.Color.Green;

                LogMessage("✓ ITaskbarList3 初始化成功");
                LogMessage("可以开始测试任务栏进度");

                // 设置初始状态
                _controller.SetState(TaskbarInteropPrivate.TBPFLAG.TBPF_NORMAL);
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"✗ 初始化失败\n线程单元：{apartment} | 进程位数：{bitness}";
                lblStatus.ForeColor = System.Drawing.Color.Red;

                LogMessage("✗ 初始化失败");
                LogMessage($"错误类型: {ex.GetType().Name}");
                LogMessage($"错误消息: {ex.Message}");

                try
                {
                    int hresult = Marshal.GetHRForException(ex);
                    LogMessage($"HRESULT: 0x{hresult:X8}");
                }
                catch { }

                if (ex.InnerException != null)
                {
                    LogMessage($"内部异常: {ex.InnerException.Message}");
                }

                LogMessage($"堆栈跟踪:\n{ex.StackTrace}");

                MessageBox.Show(
                    $"初始化失败：{ex.Message}\n\n" +
                    $"请查看窗口中的详细日志信息。",
                    "错误",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void TrackBar_ValueChanged(object? sender, EventArgs e)
        {
            if (_controller == null) return;

            try
            {
                int value = trackBar.Value;
                lblProgress.Text = $"进度: {value}%";

                _controller.SetValue((ulong)value, 100);
                LogMessage($"更新进度: {value}%");
            }
            catch (Exception ex)
            {
                LogMessage($"✗ 更新进度失败: {ex.Message}");
            }
        }

        private void SetState(TaskbarInteropPrivate.TBPFLAG state)
        {
            if (_controller == null)
            {
                LogMessage("✗ 控制器未初始化");
                return;
            }

            try
            {
                _controller.SetState(state);
                string stateName = state switch
                {
                    TaskbarInteropPrivate.TBPFLAG.TBPF_NORMAL => "正常 (绿色)",
                    TaskbarInteropPrivate.TBPFLAG.TBPF_PAUSED => "暂停 (黄色)",
                    TaskbarInteropPrivate.TBPFLAG.TBPF_ERROR => "错误 (红色)",
                    TaskbarInteropPrivate.TBPFLAG.TBPF_INDETERMINATE => "不确定",
                    _ => "无进度"
                };

                LogMessage($"✓ 切换状态: {stateName}");

                // 更新状态标签
                var apartment = Thread.CurrentThread.GetApartmentState();
                var bitness = Environment.Is64BitProcess ? "x64" : "x86";
                lblStatus.Text = $"✓ 状态：已连接 ({stateName})\n" +
                               $"线程单元：{apartment} | 进程位数：{bitness}\n" +
                               $"句柄：0x{this.Handle:X}";
            }
            catch (Exception ex)
            {
                LogMessage($"✗ 设置状态失败: {ex.Message}");
            }
        }

        private void ClearProgress()
        {
            if (_controller == null)
            {
                LogMessage("✗ 控制器未初始化");
                return;
            }

            try
            {
                _controller.Clear();
                trackBar.Value = 0;
                LogMessage("✓ 已清除任务栏进度");
            }
            catch (Exception ex)
            {
                LogMessage($"✗ 清除进度失败: {ex.Message}");
            }
        }

        private void LogMessage(string message)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
            string line = $"[{timestamp}] {message}";

            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action(() =>
                {
                    txtLog.AppendText(line + Environment.NewLine);
                }));
            }
            else
            {
                txtLog.AppendText(line + Environment.NewLine);
            }
        }

        private void TaskbarDebugForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            _controller?.Dispose();
            LogMessage("========== 窗体关闭 ==========");
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(600, 500);
            this.Name = "TaskbarDebugForm";
            this.ResumeLayout(false);
        }
    }

    // ==================== COM 互操作层 ====================
    internal static class TaskbarInteropPrivate
    {
        private static readonly Guid CLSID_TaskbarList = new("56FDF344-FD6D-11D0-958A-006097C9A090");
        private static readonly Guid IID_ITaskbarList3 = new("EA1AFB91-9E28-4B86-90E9-9E9F8A5EEA84");

        [Flags]
        internal enum TBPFLAG : uint
        {
            TBPF_NOPROGRESS = 0x0,
            TBPF_INDETERMINATE = 0x1,
            TBPF_NORMAL = 0x2,
            TBPF_ERROR = 0x4,
            TBPF_PAUSED = 0x8
        }

        // P/Invoke 声明
        [DllImport("ole32.dll", PreserveSig = true)]
        private static extern int CoCreateInstance(
            ref Guid rclsid,
            IntPtr pUnkOuter,
            uint dwClsContext,
            ref Guid riid,
            out IntPtr ppv);

        private const uint CLSCTX_INPROC_SERVER = 0x1;
        private const uint CLSCTX_LOCAL_SERVER = 0x4;
        private const uint CLSCTX_ALL = CLSCTX_INPROC_SERVER | CLSCTX_LOCAL_SERVER;

        // COM 接口定义（必须保持 vtable 顺序）
        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("EA1AFB91-9E28-4B86-90E9-9E9F8A5EEA84")]
        private interface ITaskbarList3
        {
            // ITaskbarList 方法
            void HrInit();
            void AddTab(IntPtr hwnd);
            void DeleteTab(IntPtr hwnd);
            void ActivateTab(IntPtr hwnd);
            void SetActiveAlt(IntPtr hwnd);

            // ITaskbarList2 方法
            void MarkFullscreenWindow(IntPtr hwnd, [MarshalAs(UnmanagedType.Bool)] bool fFullscreen);

            // ITaskbarList3 方法
            void SetProgressValue(IntPtr hwnd, ulong ullCompleted, ulong ullTotal);
            void SetProgressState(IntPtr hwnd, TBPFLAG tbpFlags);
        }

        internal sealed class TaskbarController : IDisposable
        {
            private readonly ITaskbarList3? _taskbar;
            private readonly IntPtr _hwnd;
            private IntPtr _pUnknown = IntPtr.Zero;

            public TaskbarController(IntPtr hwnd)
            {
                _hwnd = hwnd;

                try
                {
                    // 1. 调用 CoCreateInstance 获取 COM 对象指针
                    Guid clsid = CLSID_TaskbarList;
                    Guid iid = IID_ITaskbarList3;

                    int hr = CoCreateInstance(
                        ref clsid,
                        IntPtr.Zero,
                        CLSCTX_ALL,
                        ref iid,
                        out _pUnknown);

                    if (hr < 0 || _pUnknown == IntPtr.Zero)
                    {
                        throw new COMException($"CoCreateInstance 失败，HRESULT=0x{hr:X8}", hr);
                    }

                    // 2. 将 IUnknown 指针转换为托管接口
                    _taskbar = (ITaskbarList3)Marshal.GetTypedObjectForIUnknown(_pUnknown, typeof(ITaskbarList3));

                    // 3. 初始化 Taskbar
                    _taskbar.HrInit();
                }
                catch (Exception ex)
                {
                    // 清理资源
                    if (_pUnknown != IntPtr.Zero)
                    {
                        Marshal.Release(_pUnknown);
                        _pUnknown = IntPtr.Zero;
                    }
                    throw new InvalidOperationException($"初始化 ITaskbarList3 失败：{ex.Message}", ex);
                }
            }

            public void SetState(TBPFLAG state)
            {
                _taskbar?.SetProgressState(_hwnd, state);
            }

            public void SetValue(ulong value, ulong total)
            {
                _taskbar?.SetProgressValue(_hwnd, value, total);
            }

            public void Clear()
            {
                _taskbar?.SetProgressState(_hwnd, TBPFLAG.TBPF_NOPROGRESS);
            }

            public void Dispose()
            {
                try
                {
                    Clear();
                }
                catch { }
                finally
                {
                    if (_pUnknown != IntPtr.Zero)
                    {
                        Marshal.Release(_pUnknown);
                        _pUnknown = IntPtr.Zero;
                    }
                }
            }
        }
    }
}
