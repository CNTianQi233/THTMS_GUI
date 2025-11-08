using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace THTMS_GUI
{
    public partial class Form1 : Form
    {
        // 声明全局变量
        private CancellationTokenSource cancellationTokenSource;
        private bool _isTaskRunning = false;

        // ========【新增】任务栏进度包装========
        private TaskbarInterop.TaskbarProgress _taskbar; // TaskbarList3
        // ======================================

        // ================== 通用/Graphics 部分 ==================
        private static readonly byte[][] _yg = new byte[][]
        {
            new byte[] { 202, 144, 216, 211, 188, 214, 244, 180, 174, 194, 188, 101, 40, 22, 189, 183 },
            new byte[] { 61, 88, 123, 71, 96, 236, 242, 152, 50, 42, 213, 5, 171, 156, 175, 190 },
            new byte[] { 77, 60, 63, 80, 18, 58, 69, 119, 137, 140, 117, 143, 126, 158, 17, 135 },
            new byte[] { 242, 165, 41, 132, 231, 150, 148, 35, 220, 80, 204, 233, 36, 174, 251, 77 }
        };

        private static readonly string[] KeyDescriptions = new[]
        {
            "0 - 默认密钥（.xna纹理）",
            "1 - XOR（游戏数据）",
            "2 - 备用密钥（未知）",
            "3 - 回放（.rpy）"
        };

        private static readonly Dictionary<string, byte[]> FileSignatures =
            new Dictionary<string, byte[]>(StringComparer.OrdinalIgnoreCase)
            {
                { ".png",  new byte[] { 0x89, 0x50, 0x4E, 0x47 } },
                { ".jpg",  new byte[] { 0xFF, 0xD8, 0xFF } },
                { ".jpeg", new byte[] { 0xFF, 0xD8, 0xFF } },
                { ".bmp",  new byte[] { 0x42, 0x4D } },
                { ".tga",  new byte[] { 0x00, 0x00, 0x02 } },
                { ".dds",  new byte[] { 0x44, 0x44, 0x53, 0x20 } },
                { ".txt",  new byte[] { } }
            };

        private static readonly HashSet<String> ImageExtsNoDDS =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { ".png", ".jpg", ".jpeg", ".bmp", ".tga", ".gif", ".hdr" };

        private readonly object _consoleLock = new object();
        private readonly int _maxParallel = Math.Max(1, Environment.ProcessorCount * 2);

        private ConcurrentBag<FileProcessInfo> _processInfoList = new ConcurrentBag<FileProcessInfo>();
        private static readonly string GListRootDir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "THMHJIIIResourceHacker");

        private readonly List<string> _graphicsSelectedFiles = new List<string>();
        private string _graphicsRecursiveRoot = null;

        // ================== Data 部分 ==================
        private static readonly byte[][] DataKeys = new byte[][]
        {
            new byte[] { 202,144,216,211,188,214,244,180,174,194,188,101,40,22,189,183 },
            new byte[] { 77,60,63,80,18,58,69,119,137,140,117,143,126,158,17,135 }
        };

        private static readonly string[] DataUnpackExts = { ".dat", ".xna" };
        private static readonly string[] DataPackExts = { ".txt", ".dds", ".png", ".jpg", ".bmp", ".tga", ".gif", ".hdr" };

        private static readonly Dictionary<string, byte[]> DataImageSigs = new Dictionary<string, byte[]>
        {
            {"png", new byte[] {0x89,0x50,0x4E,0x47}},
            {"jpg", new byte[] {0xFF,0xD8,0xFF}},
            {"bmp", new byte[] {0x42,0x4D}},
            {"tga", new byte[] {0x00,0x00,0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}},
            {"gif", new byte[] {0x47,0x49,0x46,0x38}},
            {"hdr", new byte[] {0x23,0x3F,0x52,0x41,0x44,0x49,0x41,0x4E,0x43,0x45}}
        };
        private static readonly byte[] DDS_SIG = { 0x44, 0x44, 0x53, 0x20 };

        private readonly List<string> _dataSelectedFiles = new List<string>();

        // ================== BGM 分区 ==================
        private static readonly byte[] AudioKey = new byte[]
        { 61, 88, 123, 71, 96, 236, 242, 152, 50, 42, 213, 5, 171, 156, 175, 190 };

        private static readonly Dictionary<int, (int loopStart, int loopLength)> LoopInfo =
            new Dictionary<int, (int, int)>
            {
                {1, (7497, 9341703)}, {2, (39690, 10161030)}, {3, (72324, 8175376)},
                {4, (17599, 9209521)}, {5, (35580, 12262210)}, {6, (33575, 9168895)},
                {7, (0, 8101170)}, {8, (583884, 7147256)}, {9, (0, 0)},
                {10, (5172930, 5089960)}, {11, (0, 0)}, {12, (3972072, 6290219)},
                {13, (0, 0)}, {14, (0, 0)}, {15, (9953674, 584320)},
                {16, (1151560, 9831000)}, {17, (9452274, 292160)}
            };

        private readonly List<string> _bgmSelectedFiles = new List<string>();

        // ================== DDS 分区 ==================
        private static readonly Dictionary<string, byte[]> DDS_ImageFormatSignatures =
            new Dictionary<string, byte[]>()
            {
                {"png", new byte[] {0x89, 0x50, 0x4E, 0x47}},
                {"jpg", new byte[] {0xFF, 0xD8, 0xFF}},
                {"jpeg", new byte[] {0xFF, 0xD8, 0xFF}},
                {"bmp", new byte[] {0x42, 0x4D}},
                {"tga", new byte[] {0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00}},
                {"hdr", new byte[] {0x23, 0x3F, 0x52, 0x41, 0x44, 0x49, 0x41, 0x4E, 0x43, 0x45, 0x20, 0x46, 0x49, 0x4C, 0x45}},
                {"tif", new byte[] {0x49, 0x49, 0x2A, 0x00}},
                {"tiff", new byte[] {0x49, 0x49, 0x2A, 0x00}},
                {"jxr", new byte[] {0x49, 0x49, 0xBC, 0x00}}
            };

        private static readonly byte[] DDS_DdsSignature = new byte[] { 0x44, 0x44, 0x53, 0x20 };
        private static readonly string DDS_DdsExtension = ".dds";
        private static readonly string DDS_TexconvExe = "texconv.exe";
        private static readonly string DDS_BaseResourceDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "THMHJIIIResourceHacker");
        private static readonly string DDS_TexconvDir = Path.Combine(DDS_BaseResourceDir, "texconv");
        private static readonly string DDS_TexconvPath = Path.Combine(DDS_TexconvDir, DDS_TexconvExe);
        private static readonly string DDS_TexconvDownloadUrl = "https://github.com/microsoft/DirectXTex/releases/download/jul2025/texconv.exe";

        private static readonly HashSet<string> DDS_SupportedImageExts =
            new HashSet<string>(DDS_ImageFormatSignatures.Keys.Select(k => $".{k}"), StringComparer.OrdinalIgnoreCase);

        private HashSet<string> _ddsExternalSkip = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private HashSet<string> _ddsEffectiveSkip = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        private static readonly HashSet<string> DDS_BuiltInListExcludes = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            // ...（原有长名单保持不变）
            "5CS-10.png", "5CS-11.png", "5CS-12.png", "5CS-13.png"
        };

        private static readonly HashSet<string> DDS_BuiltInGlistExcludes = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "arrow.png", "b10.png", "b5.png", "b7.png", "b9.png", "bless_en.png", "bless_ru.png",
            "bless.png", "bottombar.png", "crapwise.png", "judge.png", "mist.png", "number.png",
            "numlogo.png", "rep.png", "smoke.png"
        };

        private readonly List<string> _ddsSelectedFiles = new List<string>();
        private string _ddsRecursiveRoot = null;

        private class GpuDevice
        {
            public int Index;
            public string NameRaw;
            public string NameDisplay;
            public override string ToString() => NameDisplay ?? NameRaw ?? ("GPU " + Index);
        }

        private List<GpuDevice> _ddsGpuDevices = new List<GpuDevice>();

        private int _progressTotal = 0;
        private int _progressCurrent = 0;

        public Form1()
        {
            InitializeComponent();

            cmbGraphicsKey.Items.AddRange(KeyDescriptions);
            if (cmbGraphicsKey.Items.Count > 0) cmbGraphicsKey.SelectedIndex = 0;

            try { if (!Directory.Exists(GListRootDir)) Directory.CreateDirectory(GListRootDir); } catch { }

            if (cmbDDSImgFormat != null)
            {
                var fmts = DDS_ImageFormatSignatures.Keys.ToList();
                var order = new[] { "png", "jpg", "bmp", "tga", "hdr", "tif", "tiff", "jxr", "jpeg" };
                fmts = order.Where(f => fmts.Contains(f)).Concat(fmts.Except(order)).Distinct().ToList();
                cmbDDSImgFormat.Items.Clear();
                foreach (var f in fmts) cmbDDSImgFormat.Items.Add(f.ToUpper());
                if (cmbDDSImgFormat.Items.Count > 0) cmbDDSImgFormat.SelectedIndex = 0;
            }

            cmbDDSGpu.Enabled = false;
            chkDDSGPU.CheckedChanged += chkDDSGPU_CheckedChanged;

            progressBarMain.Minimum = 0;
            progressBarMain.Maximum = 100;
            progressBarMain.Value = 0;
        }

        // ========【新增】确保句柄创建后初始化任务栏对象========
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            try
            {
                _taskbar?.Dispose();
                _taskbar = new TaskbarInterop.TaskbarProgress(this.Handle);
                _taskbar.Clear();
            }
            catch { /* ignore */ }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            try { _taskbar?.Dispose(); } catch { }
            base.OnFormClosed(e);
        }
        // =======================================================

        // ================== 任务控制：UI锁定/解锁 ==================
        private void LockUI()
        {
            _isTaskRunning = true;
            SafeInvoke(() =>
            {
                // 锁定所有交互控件
                panelMain.Enabled = false;
                btnForceStop.Visible = true;
                btnForceStop.Enabled = true;

                // 【TaskbarList3】未知总量时先用不确定进度
                _taskbar?.SetState(TaskbarInterop.TBPFLAG.TBPF_INDETERMINATE);
            });
        }

        private void UnlockUI()
        {
            _isTaskRunning = false;
            SafeInvoke(() =>
            {
                panelMain.Enabled = true;
                btnForceStop.Visible = false;
                btnForceStop.Enabled = false;

                // 【TaskbarList3】清理任务栏状态
                _taskbar?.Clear();
            });
        }

        private void btnForceStop_Click(object sender, EventArgs e)
        {
            if (cancellationTokenSource != null && !cancellationTokenSource.IsCancellationRequested)
            {
                cancellationTokenSource.Cancel();
                SafeInvoke(() =>
                {
                    btnForceStop.Text = "正在停止...";
                    btnForceStop.Enabled = false;

                    // 【TaskbarList3】暂停/取消时显示黄色暂停
                    _taskbar?.SetState(TaskbarInterop.TBPFLAG.TBPF_PAUSED);
                });
            }
        }

        // ================== 进度条工具 ==================
        private void StartProgress(int total)
        {
            _progressTotal = Math.Max(1, total);
            _progressCurrent = 0;
            SafeInvoke(() =>
            {
                progressBarMain.Maximum = _progressTotal;
                progressBarMain.Value = 0;
                progressBarMain.Style = ProgressBarStyle.Blocks;

                // 【TaskbarList3】初始化为正常进度（绿色）
                _taskbar?.SetState(TaskbarInterop.TBPFLAG.TBPF_NORMAL);
                _taskbar?.SetValue(0, (ulong)_progressTotal);
            });
        }

        private void StepProgress()
        {
            int v = Interlocked.Increment(ref _progressCurrent);
            SafeInvoke(() =>
            {
                if (v <= progressBarMain.Maximum)
                {
                    progressBarMain.Value = v;

                    // 【TaskbarList3】同步数值
                    _taskbar?.SetValue((ulong)v, (ulong)progressBarMain.Maximum);
                }
            });
        }

        private void EndProgress()
        {
            SafeInvoke(() =>
            {
                progressBarMain.Value = Math.Min(progressBarMain.Maximum, _progressTotal);

                // 【TaskbarList3】完成后清理（先到 100%，再清除）
                _taskbar?.SetValue((ulong)_progressTotal, (ulong)_progressTotal);
                _taskbar?.Clear();

                // 闪烁红色提示完成
                FlashProgressBarRed();
            });
        }

        private async void FlashProgressBarRed()
        {
            Color original = progressBarMain.ForeColor;
            try
            {
                progressBarMain.ForeColor = Color.Red;
                await Task.Delay(200);
                progressBarMain.ForeColor = original;
            }
            catch { }
        }

        private void SafeInvoke(Action a)
        {
            try
            {
                if (InvokeRequired) BeginInvoke(a);
                else a();
            }
            catch { }
        }

        private struct FileProcessInfo
        {
            public string SourcePath { get; }
            public string OutputPath { get; }
            public string Operation { get; }
            public string Status { get; }
            public int Size { get; }
            public double ElapsedSeconds { get; }
            public DateTime ProcessTime { get; }

            public FileProcessInfo(string source, string output, string operation, string status, int size, double elapsedSeconds, DateTime time)
            {
                SourcePath = source;
                OutputPath = output;
                Operation = operation;
                Status = status;
                Size = size;
                ElapsedSeconds = elapsedSeconds;
                ProcessTime = time;
            }
        }
        // =========================================================
        // Graphics 部分（以下均为你原有逻辑，略去注释调整，仅在进度函数里同步了任务栏）
        // =========================================================
        private void btnGraphicsIn_Click(object sender, EventArgs e)
        {
            _graphicsSelectedFiles.Clear();
            _graphicsRecursiveRoot = null;

            if (chkGraphicsRecursive.Checked)
            {
                using (var fbd = new FolderBrowserDialog() { Description = "选择根文件夹（将递归遍历）" })
                {
                    if (fbd.ShowDialog(this) == DialogResult.OK)
                    {
                        try
                        {
                            _graphicsRecursiveRoot = fbd.SelectedPath;
                            var all = Directory.GetFiles(_graphicsRecursiveRoot, "*.*", SearchOption.AllDirectories);
                            _graphicsSelectedFiles.AddRange(all);
                            tbGraphicsIn.Text = $"{_graphicsRecursiveRoot}（递归，已收集 {_graphicsSelectedFiles.Count} 个文件）";
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(this, "遍历失败：" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                using (var ofd = new OpenFileDialog()
                {
                    Multiselect = true,
                    Filter = "所有文件|*.*",
                    Title = "选择需要处理的文件（可多选）"
                })
                {
                    if (ofd.ShowDialog(this) == DialogResult.OK)
                    {
                        _graphicsSelectedFiles.AddRange(ofd.FileNames);
                        UpdateMultiSelectText(tbGraphicsIn, _graphicsSelectedFiles);
                    }
                }
            }
        }

        private void btnGraphicsOut_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog() { Description = "选择输出目录" })
            {
                if (fbd.ShowDialog(this) == DialogResult.OK) tbGraphicsOut.Text = fbd.SelectedPath;
            }
        }

        private async void btnGraphicsUnpack_Click(object sender, EventArgs e)
        {
            if (!EnsureGraphicsInput()) return;

            cancellationTokenSource = new CancellationTokenSource();
            LockUI();

            try
            {
                int keyIndex = cmbGraphicsKey.SelectedIndex < 0 ? 0 : cmbGraphicsKey.SelectedIndex;

                string outputRoot = tbGraphicsOut.Text.Trim();
                if (chkGraphicsRecursive.Checked)
                {
                    if (string.IsNullOrWhiteSpace(outputRoot)) outputRoot = _graphicsRecursiveRoot;
                    Directory.CreateDirectory(outputRoot);
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(outputRoot)) Directory.CreateDirectory(outputRoot);
                    else outputRoot = null;
                }

                var files = _graphicsSelectedFiles.ToArray();
                int success = 0, deleted = 0;
                var failed = new List<string>();
                bool deleteAfter = chkGraphicsDelete.Checked;

                var swAll = Stopwatch.StartNew();
                StartProgress(files.Length);

                await Task.Run(() =>
                {
                    Parallel.ForEach(files, new ParallelOptions { MaxDegreeOfParallelism = _maxParallel }, file =>
                    {
                        if (cancellationTokenSource.Token.IsCancellationRequested) return;

                        try
                        {
                            var sw = Stopwatch.StartNew();
                            var decrypted = DecryptFile(file, keyIndex);
                            sw.Stop();

                            string outDir;
                            string baseName;

                            if (chkGraphicsRecursive.Checked && !string.IsNullOrEmpty(_graphicsRecursiveRoot))
                            {
                                string rel = GetRelativePath(_graphicsRecursiveRoot, file);
                                string relDir = Path.GetDirectoryName(rel);
                                baseName = Path.GetFileNameWithoutExtension(rel);
                                outDir = string.IsNullOrEmpty(relDir) ? outputRoot : Path.Combine(outputRoot, relDir);
                            }
                            else
                            {
                                outDir = outputRoot ?? Path.GetDirectoryName(file);
                                baseName = Path.GetFileNameWithoutExtension(file);
                            }

                            string tempExt = GetTempOutputExtension(file, keyIndex);
                            string detectedExt = DetectFileExtension(decrypted);
                            string finalExt = !string.IsNullOrEmpty(detectedExt) ? detectedExt : tempExt;

                            Directory.CreateDirectory(outDir);
                            string finalPath = Path.Combine(outDir, baseName + finalExt);
                            File.WriteAllBytes(finalPath, decrypted);

                            Interlocked.Increment(ref success);
                            _processInfoList.Add(new FileProcessInfo(
                                file, finalPath, "解包", "成功", decrypted.Length, sw.Elapsed.TotalSeconds, DateTime.Now));

                            if (deleteAfter && DeleteSourceFile(file)) Interlocked.Increment(ref deleted);
                        }
                        catch (Exception ex)
                        {
                            lock (_consoleLock) failed.Add($"{Path.GetFileName(file)}: {ex.Message}");
                        }
                        finally
                        {
                            StepProgress();
                        }
                    });
                }, cancellationTokenSource.Token);

                swAll.Stop();

                GenerateGListFile();

                var msg = $"解包完成：成功 {success}/{files.Length} 个，耗时 {swAll.Elapsed.TotalSeconds:F2}s";
                if (deleteAfter) msg += $"\n已删除源文件：{deleted} 个";
                if (failed.Count > 0) msg += $"\n失败 {failed.Count} 个：\n" + string.Join("\n", failed.Take(8)) + (failed.Count > 8 ? "\n…" : "");
                EndProgress();
                MessageBox.Show(this, msg, "Graphics 解包", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (OperationCanceledException)
            {
                EndProgress();
                // 取消时已在 LockUI/btnForceStop_Click 设置了暂停状态；此处让消息提示即可
                MessageBox.Show(this, "操作已被用户取消", "Graphics 解包", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                UnlockUI();
                cancellationTokenSource?.Dispose();
                cancellationTokenSource = null;
            }
        }

        private async void btnGraphicsPack_Click(object sender, EventArgs e)
        {
            if (!EnsureGraphicsInput()) return;

            cancellationTokenSource = new CancellationTokenSource();
            LockUI();

            try
            {
                int keyIndex = cmbGraphicsKey.SelectedIndex < 0 ? 0 : cmbGraphicsKey.SelectedIndex;

                string outputRoot = tbGraphicsOut.Text.Trim();
                if (chkGraphicsRecursive.Checked)
                {
                    if (string.IsNullOrWhiteSpace(outputRoot)) outputRoot = _graphicsRecursiveRoot;
                    Directory.CreateDirectory(outputRoot);
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(outputRoot)) Directory.CreateDirectory(outputRoot);
                    else outputRoot = null;
                }

                var files = _graphicsSelectedFiles.ToArray();
                int success = 0, deleted = 0;
                var failed = new List<string>();
                bool deleteAfter = chkGraphicsDelete.Checked;
                string outputExt = (keyIndex == 3) ? ".rpy" : ".xna";

                var swAll = Stopwatch.StartNew();
                StartProgress(files.Length);

                await Task.Run(() =>
                {
                    Parallel.ForEach(files, new ParallelOptions { MaxDegreeOfParallelism = _maxParallel }, file =>
                    {
                        if (cancellationTokenSource.Token.IsCancellationRequested) return;

                        try
                        {
                            var sw = Stopwatch.StartNew();

                            string outDir;
                            string baseName;

                            if (chkGraphicsRecursive.Checked && !string.IsNullOrEmpty(_graphicsRecursiveRoot))
                            {
                                string rel = GetRelativePath(_graphicsRecursiveRoot, file);
                                string relDir = Path.GetDirectoryName(rel);
                                baseName = Path.GetFileNameWithoutExtension(rel);
                                outDir = string.IsNullOrEmpty(relDir) ? outputRoot : Path.Combine(outputRoot, relDir);
                            }
                            else
                            {
                                outDir = outputRoot ?? Path.GetDirectoryName(file);
                                baseName = Path.GetFileNameWithoutExtension(file);
                            }

                            Directory.CreateDirectory(outDir);
                            string outPath = Path.Combine(outDir, baseName + outputExt);

                            var encrypted = EncryptFile(file, keyIndex);
                            File.WriteAllBytes(outPath, encrypted);
                            sw.Stop();

                            Interlocked.Increment(ref success);
                            _processInfoList.Add(new FileProcessInfo(
                                file, outPath, "打包", "成功", encrypted.Length, sw.Elapsed.TotalSeconds, DateTime.Now));

                            if (deleteAfter && DeleteSourceFile(file)) Interlocked.Increment(ref deleted);
                        }
                        catch (Exception ex)
                        {
                            lock (_consoleLock) failed.Add($"{Path.GetFileName(file)}: {ex.Message}");
                        }
                        finally
                        {
                            StepProgress();
                        }
                    });
                }, cancellationTokenSource.Token);

                swAll.Stop();
                GenerateGListFile();

                var msg = $"打包完成：成功 {success}/{files.Length} 个，耗时 {swAll.Elapsed.TotalSeconds:F2}s";
                if (deleteAfter) msg += $"\n已删除源文件：{deleted} 个";
                if (failed.Count > 0) msg += $"\n失败 {failed.Count} 个：\n" + string.Join("\n", failed.Take(8)) + (failed.Count > 8 ? "\n…" : "");
                EndProgress();
                MessageBox.Show(this, msg, "Graphics 打包", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (OperationCanceledException)
            {
                EndProgress();
                MessageBox.Show(this, "操作已被用户取消", "Graphics 打包", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                UnlockUI();
                cancellationTokenSource?.Dispose();
                cancellationTokenSource = null;
            }
        }

        private void btnGraphicsKeyInfo_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            sb.AppendLine("=== 密钥信息 ===");
            for (int i = 0; i < KeyDescriptions.Length; i++)
                sb.AppendLine($"{i}. {KeyDescriptions[i]}");
            sb.AppendLine("\n提示：0常用于纹理(.xna)，1用于数据XOR，3用于回放(.rpy)。");
            MessageBox.Show(this, sb.ToString(), "密钥说明", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool EnsureGraphicsInput()
        {
            if (_graphicsSelectedFiles.Count == 0 || _graphicsSelectedFiles.Any(p => !File.Exists(p)))
            {
                MessageBox.Show(this, "请先选择要处理的源文件（可多选 / 或勾选递归后选择文件夹）。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (chkGraphicsRecursive.Checked && string.IsNullOrEmpty(_graphicsRecursiveRoot))
            {
                MessageBox.Show(this, "请先选择递归的根目录。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private void UpdateMultiSelectText(TextBox tb, List<string> files)
        {
            if (files == null || files.Count == 0) { tb.Text = string.Empty; return; }
            if (files.Count == 1) tb.Text = files[0];
            else tb.Text = $"（已选择 {files.Count} 个文件）";
        }

        private byte[] DecryptFile(string filePath, int keyIndex)
        {
            byte[] fileData = File.ReadAllBytes(filePath);

            if (keyIndex == 1)
            {
                for (int i = 128; i < fileData.Length; i++)
                    fileData[i] ^= _yg[keyIndex][i % _yg[keyIndex].Length];
                return fileData;
            }
            else
            {
                try
                {
                    using (var tripleDES = new TripleDESCryptoServiceProvider())
                    {
                        tripleDES.Key = _yg[keyIndex];
                        tripleDES.Mode = CipherMode.ECB;
                        tripleDES.Padding = PaddingMode.None;
                        using (var decryptor = tripleDES.CreateDecryptor())
                        {
                            return decryptor.TransformFinalBlock(fileData, 0, fileData.Length);
                        }
                    }
                }
                catch (CryptographicException)
                {
                    return fileData;
                }
            }
        }

        private byte[] EncryptFile(string filePath, int keyIndex)
        {
            byte[] fileData = File.ReadAllBytes(filePath);

            if (keyIndex == 1)
            {
                for (int i = 128; i < fileData.Length; i++)
                    fileData[i] ^= _yg[keyIndex][i % _yg[keyIndex].Length];
                return fileData;
            }
            else
            {
                using (var tripleDES = new TripleDESCryptoServiceProvider())
                {
                    tripleDES.Key = _yg[keyIndex];
                    tripleDES.Mode = CipherMode.ECB;
                    tripleDES.Padding = PaddingMode.PKCS7;
                    using (var encryptor = tripleDES.CreateEncryptor())
                    {
                        return encryptor.TransformFinalBlock(fileData, 0, fileData.Length);
                    }
                }
            }
        }

        private string DetectFileExtension(byte[] decryptedData)
        {
            try
            {
                if (decryptedData == null || decryptedData.Length < 4) return null;

                foreach (var kvp in FileSignatures)
                {
                    if (kvp.Key == ".txt") continue;
                    if (decryptedData.Length < kvp.Value.Length) continue;

                    bool match = true;
                    for (int i = 0; i < kvp.Value.Length; i++)
                    {
                        if (decryptedData[i] != kvp.Value[i]) { match = false; break; }
                    }
                    if (match) return kvp.Key;
                }

                bool isText = decryptedData.Take(Math.Min(1024, decryptedData.Length)).All(b => b < 0x80);
                if (isText) return ".txt";
                return null;
            }
            catch { return null; }
        }

        private string GetTempOutputExtension(string filePath, int keyIndex)
        {
            string originalExt = Path.GetExtension(filePath).ToLower();
            if (keyIndex == 0 && (originalExt == ".xna" || originalExt == ".rpy"))
                return ".dds";
            if (originalExt == ".xna" || originalExt == ".rpy")
                return ".tmp";
            return originalExt;
        }

        private bool DeleteSourceFile(string sourcePath)
        {
            try { if (File.Exists(sourcePath)) { File.Delete(sourcePath); return true; } return false; }
            catch { return false; }
        }

        private void GenerateGListFile()
        {
            try
            {
                if (_processInfoList.Count == 0) return;
                if (!Directory.Exists(GListRootDir)) Directory.CreateDirectory(GListRootDir);

                string timeStamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                string glistPath = Path.Combine(GListRootDir, $"glist_{timeStamp}.txt");

                var images = _processInfoList
                    .Where(i => ImageExtsNoDDS.Contains(Path.GetExtension(i.OutputPath)))
                    .Select(i => Path.GetFileName(i.OutputPath))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .OrderBy(x => x);

                File.WriteAllLines(glistPath, images, Encoding.UTF8);
                _processInfoList = new ConcurrentBag<FileProcessInfo>();
            }
            catch { }
        }

        private string GetRelativePath(string rootDir, string filePath)
        {
            string normalizedRoot = Path.GetFullPath(rootDir).TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;
            string normalizedFile = Path.GetFullPath(filePath);
            if (!normalizedFile.StartsWith(normalizedRoot, StringComparison.OrdinalIgnoreCase)) return filePath;
            return normalizedFile.Substring(normalizedRoot.Length);
        }

        // =========================================================
        // Data 部分
        // =========================================================
        private void btnDataIn_Click(object sender, EventArgs e)
        {
            _dataSelectedFiles.Clear();
            using (var ofd = new OpenFileDialog()
            {
                Multiselect = true,
                Filter = "Data 支持|*.dat;*.xna;*.txt;*.dds;*.png;*.jpg;*.bmp;*.tga;*.gif;*.hdr|所有文件|*.*",
                Title = "选择 Data 源文件（可多选）"
            })
            {
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    _dataSelectedFiles.AddRange(ofd.FileNames);
                    UpdateMultiSelectText(tbDataIn, _dataSelectedFiles);
                }
            }
        }

        private void btnDataOut_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog() { Description = "选择 Data 输出目录（用于打包/解包输出）" })
            {
                if (fbd.ShowDialog(this) == DialogResult.OK) tbDataOut.Text = fbd.SelectedPath;
            }
        }

        private async void btnDataUnpack_Click(object sender, EventArgs e)
        {
            if (!EnsureDataInput(out string outDir)) return;

            cancellationTokenSource = new CancellationTokenSource();
            LockUI();

            try
            {
                var files = _dataSelectedFiles
                    .Where(f => DataUnpackExts.Contains(Path.GetExtension(f).ToLower()))
                    .ToArray();

                if (files.Length == 0)
                {
                    MessageBox.Show(this, "未选择可解包的文件（仅支持 .dat/.xna）。", "Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UnlockUI();
                    return;
                }

                Directory.CreateDirectory(outDir);

                int success = 0;
                int deleted = 0;
                List<string> imageNames = new List<string>();
                bool deleteAfter = chkDataDelete.Checked;
                var swAll = Stopwatch.StartNew();
                StartProgress(files.Length);

                await Task.Run(() =>
                {
                    Parallel.ForEach(files, new ParallelOptions { MaxDegreeOfParallelism = _maxParallel }, file =>
                    {
                        if (cancellationTokenSource.Token.IsCancellationRequested) return;

                        try
                        {
                            string ext = Path.GetExtension(file).ToLower();
                            string nameWithout = Path.GetFileNameWithoutExtension(file);
                            string outPathGuess = Path.Combine(outDir, nameWithout + (ext == ".dat" ? ".dds" : ".txt"));

                            bool ok = DataDecryptFile(file, outPathGuess, ext == ".dat" ? 1 : 2, out string finalOut);
                            if (ok)
                            {
                                Interlocked.Increment(ref success);

                                var outExt = Path.GetExtension(finalOut);
                                if (ImageExtsNoDDS.Contains(outExt))
                                {
                                    lock (imageNames) imageNames.Add(Path.GetFileName(finalOut));
                                }

                                if (deleteAfter && DeleteSourceFile(file)) Interlocked.Increment(ref deleted);
                            }
                        }
                        catch { }
                        finally { StepProgress(); }
                    });
                }, cancellationTokenSource.Token);

                swAll.Stop();

                if (imageNames.Count > 0) WriteListFile(imageNames);

                EndProgress();
                MessageBox.Show(this,
                    $"批量解包完成：成功 {success}/{files.Length} 个\n已输出目录：{outDir}\n{(deleteAfter ? $"已删除源文件：{deleted} 个\n" : "")}耗时：{swAll.Elapsed.TotalSeconds:F2}s",
                    "Data 解包", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (OperationCanceledException)
            {
                EndProgress();
                MessageBox.Show(this, "操作已被用户取消", "Data 解包", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                UnlockUI();
                cancellationTokenSource?.Dispose();
                cancellationTokenSource = null;
            }
        }

        private async void btnDataPack_Click(object sender, EventArgs e)
        {
            if (!EnsureDataInput(out string outDir)) return;

            cancellationTokenSource = new CancellationTokenSource();
            LockUI();

            try
            {
                var files = _dataSelectedFiles
                    .Where(f => DataPackExts.Contains(Path.GetExtension(f).ToLower()))
                    .ToArray();

                if (files.Length == 0)
                {
                    MessageBox.Show(this, "未选择可打包的文件（支持 .txt/.dds/.png/.jpg/.bmp/.tga/.gif/.hdr）。", "Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UnlockUI();
                    return;
                }

                Directory.CreateDirectory(outDir);

                int success = 0;
                int deleted = 0;
                bool deleteAfter = chkDataDelete.Checked;
                var swAll = Stopwatch.StartNew();
                StartProgress(files.Length);

                await Task.Run(() =>
                {
                    Parallel.ForEach(files, new ParallelOptions { MaxDegreeOfParallelism = _maxParallel }, file =>
                    {
                        if (cancellationTokenSource.Token.IsCancellationRequested) return;

                        try
                        {
                            string ext = Path.GetExtension(file).ToLower();
                            string nameWithout = Path.GetFileNameWithoutExtension(file);

                            string outExt;
                            int keyIndex;
                            if (ext == ".txt")
                            {
                                outExt = ".xna"; keyIndex = 2;
                            }
                            else
                            {
                                outExt = ".dat"; keyIndex = 1;
                            }

                            string outPath = Path.Combine(outDir, nameWithout + outExt);
                            if (DataEncryptFile(file, outPath, keyIndex))
                            {
                                Interlocked.Increment(ref success);
                                if (deleteAfter && DeleteSourceFile(file)) Interlocked.Increment(ref deleted);
                            }
                        }
                        catch { }
                        finally { StepProgress(); }
                    });
                }, cancellationTokenSource.Token);

                swAll.Stop();

                EndProgress();
                MessageBox.Show(this,
                    $"批量打包完成：成功 {success}/{files.Length} 个\n已输出目录：{outDir}\n{(deleteAfter ? $"已删除源文件：{deleted} 个\n" : "")}耗时：{swAll.Elapsed.TotalSeconds:F2}s",
                    "Data 打包", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (OperationCanceledException)
            {
                EndProgress();
                MessageBox.Show(this, "操作已被用户取消", "Data 打包", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                UnlockUI();
                cancellationTokenSource?.Dispose();
                cancellationTokenSource = null;
            }
        }

        private void btnDataList_Click(object sender, EventArgs e)
        {
            string root = tbDataOut.Text.Trim();
            if (string.IsNullOrWhiteSpace(root) || !Directory.Exists(root))
            {
                MessageBox.Show(this, "请先指定一个有效的输出路径。", "Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var names = Directory.GetFiles(root, "*.*", SearchOption.AllDirectories)
                .Where(p => ImageExtsNoDDS.Contains(Path.GetExtension(p)))
                .Select(p => Path.GetFileName(p))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(n => n)
                .ToList();

            if (names.Count == 0)
            {
                MessageBox.Show(this, "在输出目录中未找到任何图像文件，未生成 list。", "Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            WriteListFile(names);
            MessageBox.Show(this, $"已生成 list（共 {names.Count} 条）。", "Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool EnsureDataInput(out string outDir)
        {
            outDir = tbDataOut.Text.Trim();

            if (_dataSelectedFiles.Count == 0 || _dataSelectedFiles.Any(p => !File.Exists(p)))
            {
                MessageBox.Show(this, "请先在源路径（多选）选择要处理的文件。", "Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (string.IsNullOrWhiteSpace(outDir))
            {
                MessageBox.Show(this, "请先指定输出路径。", "Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private void WriteListFile(IEnumerable<string> imageNames)
        {
            try
            {
                Directory.CreateDirectory(GListRootDir);
                string ts = DateTime.Now.ToString("yyyyMMddHHmmss");
                string listPath = Path.Combine(GListRootDir, $"list_{ts}.txt");
                var uniq = imageNames
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .Select(s => s.Trim())
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .OrderBy(s => s)
                    .ToArray();
                File.WriteAllLines(listPath, uniq, Encoding.UTF8);
            }
            catch { }
        }

        private bool DataEncryptFile(string inputPath, string outputPath, int keyIndex)
        {
            try
            {
                byte[] raw = File.ReadAllBytes(inputPath);
                int idx = keyIndex == 1 ? 0 : 1;
                byte[] enc = DataTripleDES(raw, idx, true);
                File.WriteAllBytes(outputPath, enc);
                return true;
            }
            catch { return false; }
        }

        private bool DataDecryptFile(string inputPath, string outputGuessPath, int keyIndex, out string finalOut)
        {
            try
            {
                byte[] enc = File.ReadAllBytes(inputPath);
                int idx = keyIndex == 1 ? 0 : 1;
                byte[] dec = DataTripleDES(enc, idx, false);

                finalOut = outputGuessPath;
                if (keyIndex == 1 && Path.GetExtension(inputPath).Equals(".dat", StringComparison.OrdinalIgnoreCase))
                {
                    string detected = DataDetectImage(dec);
                    if (!string.IsNullOrEmpty(detected) &&
                        !string.Equals(Path.GetExtension(outputGuessPath), detected, StringComparison.OrdinalIgnoreCase))
                    {
                        finalOut = Path.ChangeExtension(outputGuessPath, detected);
                    }
                }

                File.WriteAllBytes(finalOut, dec);
                return true;
            }
            catch
            {
                finalOut = outputGuessPath;
                return false;
            }
        }

        private byte[] DataTripleDES(byte[] data, int keyArrayIndex, bool encrypt)
        {
            using (var des = new TripleDESCryptoServiceProvider())
            {
                des.Key = DataKeys[keyArrayIndex];
                des.Mode = CipherMode.ECB;
                des.Padding = PaddingMode.PKCS7;
                des.BlockSize = 64;
                ICryptoTransform tf = encrypt ? des.CreateEncryptor() : des.CreateDecryptor();
                return tf.TransformFinalBlock(data, 0, data.Length);
            }
        }

        private string DataDetectImage(byte[] buf)
        {
            if (buf == null || buf.Length < 4) return null;

            foreach (var kv in DataImageSigs)
            {
                var sig = kv.Value;
                if (buf.Length >= sig.Length && buf.Take(sig.Length).SequenceEqual(sig))
                    return "." + kv.Key;
            }
            if (buf.Length >= 4 && buf[0] == DDS_SIG[0] && buf[1] == DDS_SIG[1] && buf[2] == DDS_SIG[2] && buf[3] == DDS_SIG[3])
                return ".dds";
            return null;
        }

        // =========================================================
        // BGM 部分
        // =========================================================
        private void btnBGMIn_Click(object sender, EventArgs e)
        {
            _bgmSelectedFiles.Clear();
            using (var ofd = new OpenFileDialog()
            {
                Multiselect = true,
                Filter = "BGM 支持|*.xna;*.ogg|所有文件|*.*",
                Title = "选择 BGM 源文件（可多选）"
            })
            {
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    _bgmSelectedFiles.AddRange(ofd.FileNames);
                    UpdateMultiSelectText(tbBGMIn, _bgmSelectedFiles);
                }
            }
        }

        private void btnBGMOut_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog() { Description = "选择 BGM 输出目录" })
                if (fbd.ShowDialog(this) == DialogResult.OK) tbBGMOut.Text = fbd.SelectedPath;
        }

        private async void btnBGMUnpack_Click(object sender, EventArgs e)
        {
            if (!EnsureBGMInput(out string outDir)) return;

            cancellationTokenSource = new CancellationTokenSource();
            LockUI();

            try
            {
                var files = _bgmSelectedFiles
                    .Where(p => string.Equals(Path.GetExtension(p), ".xna", StringComparison.OrdinalIgnoreCase))
                    .ToArray();

                if (files.Length == 0)
                {
                    MessageBox.Show(this, "未选择任何 .xna 文件用于解包。", "BGM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UnlockUI();
                    return;
                }

                Directory.CreateDirectory(outDir);

                int success = 0;
                var failed = new ConcurrentBag<string>();
                var sw = Stopwatch.StartNew();
                StartProgress(files.Length);

                await Task.Run(() =>
                {
                    Parallel.ForEach(files, new ParallelOptions { MaxDegreeOfParallelism = _maxParallel }, file =>
                    {
                        if (cancellationTokenSource.Token.IsCancellationRequested) return;

                        try
                        {
                            string nameWithout = Path.GetFileNameWithoutExtension(file);
                            string outPath = Path.Combine(outDir, nameWithout + ".ogg");
                            if (ExtractSingleAudio(file, outPath)) Interlocked.Increment(ref success);
                            else failed.Add(Path.GetFileName(file));
                        }
                        catch (Exception ex)
                        {
                            failed.Add($"{Path.GetFileName(file)}: {ex.Message}");
                        }
                        finally { StepProgress(); }
                    });
                }, cancellationTokenSource.Token);

                sw.Stop();
                EndProgress();
                var msg = $"BGM 解包完成：成功 {success}/{files.Length} 个，耗时 {sw.Elapsed.TotalSeconds:F2}s";
                if (failed.Count > 0) msg += $"\n失败 {failed.Count} 个：\n" + string.Join("\n", failed.Take(8)) + (failed.Count > 8 ? "\n…" : "");
                MessageBox.Show(this, msg, "BGM 解包", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (OperationCanceledException)
            {
                EndProgress();
                MessageBox.Show(this, "操作已被用户取消", "BGM 解包", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                UnlockUI();
                cancellationTokenSource?.Dispose();
                cancellationTokenSource = null;
            }
        }

        private async void btnBGMPack_Click(object sender, EventArgs e)
        {
            if (!EnsureBGMInput(out string outDir)) return;

            cancellationTokenSource = new CancellationTokenSource();
            LockUI();

            try
            {
                var files = _bgmSelectedFiles
                    .Where(p => string.Equals(Path.GetExtension(p), ".ogg", StringComparison.OrdinalIgnoreCase))
                    .ToArray();

                if (files.Length == 0)
                {
                    MessageBox.Show(this, "未选择任何 .ogg 文件用于打包。", "BGM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UnlockUI();
                    return;
                }

                Directory.CreateDirectory(outDir);

                int success = 0;
                var failed = new ConcurrentBag<string>();
                var sw = Stopwatch.StartNew();
                StartProgress(files.Length);

                await Task.Run(() =>
                {
                    Parallel.ForEach(files, new ParallelOptions { MaxDegreeOfParallelism = _maxParallel }, file =>
                    {
                        if (cancellationTokenSource.Token.IsCancellationRequested) return;

                        try
                        {
                            int track = GetTrackNumberFromFilename(file);
                            string nameWithout = track > 0 ? track.ToString() : Path.GetFileNameWithoutExtension(file);
                            string outPath = Path.Combine(outDir, nameWithout + ".xna");

                            if (PackSingleAudio(file, outPath, track)) Interlocked.Increment(ref success);
                            else failed.Add(Path.GetFileName(file));
                        }
                        catch (Exception ex)
                        {
                            failed.Add($"{Path.GetFileName(file)}: {ex.Message}");
                        }
                        finally { StepProgress(); }
                    });
                }, cancellationTokenSource.Token);

                sw.Stop();
                EndProgress();
                var msg = $"BGM 打包完成：成功 {success}/{files.Length} 个，耗时 {sw.Elapsed.TotalSeconds:F2}s";
                if (failed.Count > 0) msg += $"\n失败 {failed.Count} 个：\n" + string.Join("\n", failed.Take(8)) + (failed.Count > 8 ? "\n…" : "");
                MessageBox.Show(this, msg, "BGM 打包", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (OperationCanceledException)
            {
                EndProgress();
                MessageBox.Show(this, "操作已被用户取消", "BGM 打包", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                UnlockUI();
                cancellationTokenSource?.Dispose();
                cancellationTokenSource = null;
            }
        }

        private bool EnsureBGMInput(out string outDir)
        {
            outDir = tbBGMOut.Text.Trim();
            if (_bgmSelectedFiles.Count == 0 || _bgmSelectedFiles.Any(p => !File.Exists(p)))
            {
                MessageBox.Show(this, "请先在源路径（多选）选择 BGM 文件（支持 .xna/.ogg）。", "BGM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (string.IsNullOrWhiteSpace(outDir))
            {
                MessageBox.Show(this, "请先指定输出路径。", "BGM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private bool ExtractSingleAudio(string inputPath, string outputPath = null)
        {
            try
            {
                if (!File.Exists(inputPath)) return false;
                if (!string.Equals(Path.GetExtension(inputPath), ".xna", StringComparison.OrdinalIgnoreCase)) return false;

                outputPath ??= Path.ChangeExtension(inputPath, ".ogg");
                string outDir = Path.GetDirectoryName(outputPath);
                if (!string.IsNullOrEmpty(outDir) && !Directory.Exists(outDir)) Directory.CreateDirectory(outDir);

                byte[] encrypted = File.ReadAllBytes(inputPath);
                byte[] decrypted = ProcessAudioXOR(encrypted);
                File.WriteAllBytes(outputPath, decrypted);

                int track = GetTrackNumberFromFilename(inputPath);
                if (track > 0 && LoopInfo.TryGetValue(track, out var li) && (li.loopStart > 0 || li.loopLength > 0))
                {
                    string loopTxt = Path.ChangeExtension(outputPath, ".txt");
                    GenerateLoopInfoFile(loopTxt, track, li.loopStart, li.loopLength);
                }
                return true;
            }
            catch { return false; }
        }

        private bool PackSingleAudio(string inputPath, string outputPath = null, int trackNumber = -1)
        {
            try
            {
                if (!File.Exists(inputPath)) return false;
                if (!string.Equals(Path.GetExtension(inputPath), ".ogg", StringComparison.OrdinalIgnoreCase)) return false;

                outputPath ??= Path.ChangeExtension(inputPath, ".xna");
                string outDir = Path.GetDirectoryName(outputPath);
                if (!string.IsNullOrEmpty(outDir) && !Directory.Exists(outDir)) Directory.CreateDirectory(outDir);

                byte[] ogg = File.ReadAllBytes(inputPath);
                byte[] encrypted = ProcessAudioXOR(ogg);
                File.WriteAllBytes(outputPath, encrypted);
                return true;
            }
            catch { return false; }
        }

        private byte[] ProcessAudioXOR(byte[] data)
        {
            if (data == null || data.Length == 0) return Array.Empty<byte>();
            byte[] buf = new byte[data.Length];
            Array.Copy(data, buf, data.Length);

            int start = Math.Min(128, buf.Length);
            for (int i = start; i < buf.Length; i++)
                buf[i] ^= AudioKey[i % AudioKey.Length];

            return buf;
        }

        private int GetTrackNumberFromFilename(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return -1;
            string name = Path.GetFileNameWithoutExtension(filePath);
            return int.TryParse(name, out int n) ? n : -1;
        }

        private void GenerateLoopInfoFile(string outputPath, int trackNumber, int loopStart, int loopLength)
        {
            try
            {
                string info = $"BGM音轨编号: {trackNumber}\n" +
                              $"循环开始位置: {loopStart} 样本\n" +
                              $"循环长度: {loopLength} 样本\n" +
                              $"循环结束位置: {loopStart + loopLength} 样本\n\n" +
                              $"说明: 样本数基于44100Hz采样率，可用(样本数/44100)换算为秒";
                File.WriteAllText(outputPath, info, Encoding.UTF8);
            }
            catch { }
        }

        // =========================================================
        // DDS 部分
        // =========================================================
        private void btnDDSIn_Click(object sender, EventArgs e)
        {
            _ddsSelectedFiles.Clear();
            _ddsRecursiveRoot = null;

            if (chkDDSRecursive.Checked)
            {
                using (var fbd = new FolderBrowserDialog() { Description = "选择根文件夹（将递归遍历）" })
                {
                    if (fbd.ShowDialog(this) == DialogResult.OK)
                    {
                        try
                        {
                            _ddsRecursiveRoot = fbd.SelectedPath;
                            var all = Directory.GetFiles(_ddsRecursiveRoot, "*.*", SearchOption.AllDirectories);
                            _ddsSelectedFiles.AddRange(all);
                            tbDDSIn.Text = $"{_ddsRecursiveRoot}（递归，已收集 {_ddsSelectedFiles.Count} 个文件）";
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(this, "遍历失败：" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                using (var ofd = new OpenFileDialog()
                {
                    Multiselect = true,
                    Filter = "所有文件|*.*|图片|*.png;*.jpg;*.jpeg;*.bmp;*.tga;*.hdr;*.tif;*.tiff;*.jxr|DDS|*.dds",
                    Title = "选择 DDS/图片 源文件（可多选）"
                })
                {
                    if (ofd.ShowDialog(this) == DialogResult.OK)
                    {
                        _ddsSelectedFiles.AddRange(ofd.FileNames);
                        UpdateMultiSelectText(tbDDSIn, _ddsSelectedFiles);
                    }
                }
            }
        }

        private void btnDDSOut_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog() { Description = "选择 DDS 输出目录" })
            {
                if (fbd.ShowDialog(this) == DialogResult.OK) tbDDSOut.Text = fbd.SelectedPath;
            }
        }

        private void btnDDSGetTexconv_Click(object sender, EventArgs e)
        {
            if (DownloadTexconv())
            {
                MessageBox.Show(this, "texconv 下载完成（或已存在）。\n路径：" + DDS_TexconvPath, "DDS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshGpuDevices();
            }
            else
            {
                MessageBox.Show(this, "下载失败，请检查网络或手动放置 texconv.exe 到：\n" + DDS_TexconvPath, "DDS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnDDSUnpack_Click(object sender, EventArgs e)
        {
            if (!EnsureDDSInput(out string outRoot)) return;
            if (!CheckTexconvExists())
            {
                MessageBox.Show(this, "未找到 texconv.exe，请先点击获取 texconv。", "DDS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            cancellationTokenSource = new CancellationTokenSource();
            LockUI();

            try
            {
                BuildEffectiveSkipSet();

                string fmt = (cmbDDSImgFormat?.SelectedItem?.ToString() ?? "PNG").ToLower();
                if (fmt == "jpeg") fmt = "jpg";

                var allFiles = _ddsSelectedFiles.ToArray();
                SearchOption searchOpt = chkDDSRecursive.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

                string[] targetFiles;
                if (chkDDSRecursive.Checked && !string.IsNullOrEmpty(_ddsRecursiveRoot))
                    targetFiles = Directory.GetFiles(_ddsRecursiveRoot, "*.dds", searchOpt);
                else
                    targetFiles = allFiles.Where(p => string.Equals(Path.GetExtension(p), ".dds", StringComparison.OrdinalIgnoreCase)).ToArray();

                if (targetFiles.Length == 0)
                {
                    MessageBox.Show(this, "未找到 .dds 文件。", "DDS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UnlockUI();
                    return;
                }

                Directory.CreateDirectory(outRoot);

                bool keepStruct = chkDDSRecursive.Checked;

                int success = 0, skipped = 0;
                var sw = Stopwatch.StartNew();
                StartProgress(targetFiles.Length);

                await Task.Run(() =>
                {
                    Parallel.ForEach(targetFiles, new ParallelOptions { MaxDegreeOfParallelism = Math.Max(2, _maxParallel) }, file =>
                    {
                        if (cancellationTokenSource.Token.IsCancellationRequested) return;

                        try
                        {
                            string processedFile = FixFileExtensionIfNeeded(file);
                            if (!string.Equals(Path.GetExtension(processedFile), ".dds", StringComparison.OrdinalIgnoreCase))
                            {
                                Interlocked.Increment(ref skipped);
                            }
                            else
                            {
                                string outDir = outRoot;
                                if (keepStruct && chkDDSRecursive.Checked && !string.IsNullOrEmpty(_ddsRecursiveRoot))
                                {
                                    string relative = Path.GetDirectoryName(GetRelativePath(_ddsRecursiveRoot, processedFile)) ?? "";
                                    outDir = Path.Combine(outRoot, relative);
                                }
                                Directory.CreateDirectory(outDir);

                                string args = $"-ft {fmt} -y -o \"{outDir}\" \"{processedFile}\"";
                                bool ok = ExecuteTexconv(args);
                                if (ok)
                                {
                                    Interlocked.Increment(ref success);
                                }
                                else
                                {
                                    Interlocked.Increment(ref skipped);
                                }
                            }
                        }
                        catch { Interlocked.Increment(ref skipped); }
                        finally { StepProgress(); }
                    });
                }, cancellationTokenSource.Token);

                sw.Stop();
                EndProgress();
                MessageBox.Show(this,
                    $"DDS → 图片 完成：成功 {success}/{targetFiles.Length}，跳过/失败 {skipped}，耗时 {sw.Elapsed.TotalSeconds:F2}s",
                    "DDS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (OperationCanceledException)
            {
                EndProgress();
                MessageBox.Show(this, "操作已被用户取消", "DDS 解包", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                UnlockUI();
                cancellationTokenSource?.Dispose();
                cancellationTokenSource = null;
            }
        }

        private async void btnDDSPack_Click(object sender, EventArgs e)
        {
            if (!EnsureDDSInput(out string outRoot)) return;
            if (!CheckTexconvExists())
            {
                MessageBox.Show(this, "未找到 texconv.exe，请先点击获取 texconv。", "DDS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            cancellationTokenSource = new CancellationTokenSource();
            LockUI();

            try
            {
                BuildEffectiveSkipSet();

                string gpuParam = "";
                if (chkDDSGPU.Checked)
                {
                    int sel = GetSelectedGpuIndex();
                    if (sel >= 0) gpuParam = $"-gpu {sel}";
                }

                var allFiles = _ddsSelectedFiles.ToArray();
                SearchOption searchOpt = chkDDSRecursive.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

                string[] targetFiles;
                if (chkDDSRecursive.Checked && !string.IsNullOrEmpty(_ddsRecursiveRoot))
                {
                    targetFiles = Directory.GetFiles(_ddsRecursiveRoot, "*.*", searchOpt)
                        .Where(f => DDS_SupportedImageExts.Contains(Path.GetExtension(f))).ToArray();
                }
                else
                {
                    targetFiles = allFiles
                        .Where(f => DDS_SupportedImageExts.Contains(Path.GetExtension(f))).ToArray();
                }

                if (_ddsEffectiveSkip.Count > 0)
                    targetFiles = targetFiles.Where(f => !_ddsEffectiveSkip.Contains(Path.GetFileName(f))).ToArray();

                if (targetFiles.Length == 0)
                {
                    MessageBox.Show(this, "未找到可转换的图片文件。", "DDS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UnlockUI();
                    return;
                }

                Directory.CreateDirectory(outRoot);
                bool keepStruct = chkDDSRecursive.Checked;

                int success = 0, fail = 0;
                var sw = Stopwatch.StartNew();
                StartProgress(targetFiles.Length);

                await Task.Run(() =>
                {
                    Parallel.ForEach(targetFiles, new ParallelOptions { MaxDegreeOfParallelism = Math.Max(2, _maxParallel) }, file =>
                    {
                        if (cancellationTokenSource.Token.IsCancellationRequested) return;

                        try
                        {
                            string outDir = outRoot;
                            if (keepStruct && chkDDSRecursive.Checked && !string.IsNullOrEmpty(_ddsRecursiveRoot))
                            {
                                string relative = Path.GetDirectoryName(GetRelativePath(_ddsRecursiveRoot, file)) ?? "";
                                outDir = Path.Combine(outRoot, relative);
                            }
                            Directory.CreateDirectory(outDir);

                            string args = $"{gpuParam} -f BC7_UNORM_SRGB -y -o \"{outDir}\" \"{file}\"".Trim();
                            bool ok = ExecuteTexconv(args);
                            if (ok) Interlocked.Increment(ref success);
                            else Interlocked.Increment(ref fail);
                        }
                        catch { Interlocked.Increment(ref fail); }
                        finally { StepProgress(); }
                    });
                }, cancellationTokenSource.Token);

                sw.Stop();
                EndProgress();
                MessageBox.Show(this,
                    $"图片 → DDS 完成：成功 {success}/{targetFiles.Length}，失败 {fail}，耗时 {sw.Elapsed.TotalSeconds:F2}s",
                    "DDS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (OperationCanceledException)
            {
                EndProgress();
                MessageBox.Show(this, "操作已被用户取消", "DDS 打包", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                UnlockUI();
                cancellationTokenSource?.Dispose();
                cancellationTokenSource = null;
            }
        }

        private bool EnsureDDSInput(out string outDir)
        {
            outDir = tbDDSOut.Text.Trim();

            if (_ddsSelectedFiles.Count == 0 && !(chkDDSRecursive.Checked && !string.IsNullOrEmpty(_ddsRecursiveRoot)))
            {
                MessageBox.Show(this, "请先选择源文件（可多选）或勾选递归模式后选择一个根目录。", "DDS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (chkDDSRecursive.Checked && string.IsNullOrEmpty(_ddsRecursiveRoot))
            {
                MessageBox.Show(this, "请先选择递归的根目录。", "DDS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (string.IsNullOrWhiteSpace(outDir))
            {
                MessageBox.Show(this, "请先指定输出路径。", "DDS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private void BuildEffectiveSkipSet()
        {
            _ddsEffectiveSkip.Clear();
            _ddsExternalSkip.Clear();

            try
            {
                if (Directory.Exists(DDS_BaseResourceDir))
                {
                    if (chkDDSUseListExclude.Checked)
                    {
                        var latestList = Directory.GetFiles(DDS_BaseResourceDir, "list_*.txt")
                            .OrderByDescending(f => new FileInfo(f).CreationTime).FirstOrDefault();
                        if (latestList != null)
                        {
                            var lines = File.ReadAllLines(latestList)
                                .Select(s => s.Trim()).Where(s => !string.IsNullOrWhiteSpace(s));
                            foreach (var s in lines) _ddsExternalSkip.Add(s);
                        }
                    }
                    if (chkDDSUseGlistExclude.Checked)
                    {
                        var latestGlist = Directory.GetFiles(DDS_BaseResourceDir, "glist_*.txt")
                            .OrderByDescending(f => new FileInfo(f).CreationTime).FirstOrDefault();
                        if (latestGlist != null)
                        {
                            var lines = File.ReadAllLines(latestGlist)
                                .Select(s => s.Trim()).Where(s => !string.IsNullOrWhiteSpace(s));
                            foreach (var s in lines) _ddsExternalSkip.Add(s);
                        }
                    }
                }
            }
            catch { }

            foreach (var s in _ddsExternalSkip) _ddsEffectiveSkip.Add(s);

            if (chkDDSUseDefaultExclude.Checked)
            {
                foreach (var s in DDS_BuiltInListExcludes) _ddsEffectiveSkip.Add(s);
                foreach (var s in DDS_BuiltInGlistExcludes) _ddsEffectiveSkip.Add(s);
            }
        }

        private string DetectFileRealFormat(string filePath)
        {
            try
            {
                if (!File.Exists(filePath)) return null;
                FileInfo fi = new FileInfo(filePath);
                if (fi.Length < 4) return null;

                int maxSigLength = Math.Max(DDS_ImageFormatSignatures.Values.Max(s => s.Length), DDS_DdsSignature.Length);
                byte[] header = new byte[maxSigLength];

                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 1024, useAsync: false))
                {
                    int read = fs.Read(header, 0, maxSigLength);
                    if (read < maxSigLength)
                        Array.Resize(ref header, read);
                }

                foreach (var kv in DDS_ImageFormatSignatures)
                {
                    var sig = kv.Value;
                    if (header.Length >= sig.Length && header.Take(sig.Length).SequenceEqual(sig))
                        return $".{kv.Key}";
                }

                if (header.Length >= DDS_DdsSignature.Length && header.Take(DDS_DdsSignature.Length).SequenceEqual(DDS_DdsSignature))
                    return DDS_DdsExtension;

                return null;
            }
            catch { return null; }
        }

        private string FixFileExtensionIfNeeded(string filePath)
        {
            try
            {
                string currentExt = Path.GetExtension(filePath).ToLower();
                string realExt = DetectFileRealFormat(filePath);

                if (realExt == null || currentExt == realExt) return filePath;

                string newPath = Path.ChangeExtension(filePath, realExt);
                if (File.Exists(newPath))
                {
                    string dir = Path.GetDirectoryName(filePath);
                    string name = Path.GetFileNameWithoutExtension(filePath);
                    int index = 1;
                    while (File.Exists(Path.Combine(dir, $"{name}_{index}{realExt}"))) index++;
                    newPath = Path.Combine(dir, $"{name}_{index}{realExt}");
                }
                File.Move(filePath, newPath);
                return newPath;
            }
            catch { return filePath; }
        }

        private bool CheckTexconvExists()
        {
            try
            {
                if (!Directory.Exists(DDS_TexconvDir)) Directory.CreateDirectory(DDS_TexconvDir);
            }
            catch { }
            return File.Exists(DDS_TexconvPath);
        }

        private bool DownloadTexconv()
        {
            try
            {
                if (!Directory.Exists(DDS_TexconvDir)) Directory.CreateDirectory(DDS_TexconvDir);

                if (File.Exists(DDS_TexconvPath))
                {
                    var dlg = MessageBox.Show(this, $"检测到 {DDS_TexconvExe} 已存在于：\n{DDS_TexconvDir}\n是否覆盖重新下载？",
                        "DDS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlg == DialogResult.No) return true;
                }

                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(DDS_TexconvDownloadUrl, DDS_TexconvPath);
                }

                FileInfo fi = new FileInfo(DDS_TexconvPath);
                if (fi.Length < 1024 * 100)
                {
                    try { File.Delete(DDS_TexconvPath); } catch { }
                    return false;
                }

                return true;
            }
            catch { return false; }
        }

        private (bool success, string error) ExecuteTexconvWithOutput(string arguments)
        {
            try
            {
                if (!CheckTexconvExists())
                    return (false, "texconv.exe 不存在！");

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = DDS_TexconvPath,
                    Arguments = arguments,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using (Process p = Process.Start(psi))
                {
                    string error = p.StandardError.ReadToEnd();
                    p.WaitForExit(30000);
                    return (p.ExitCode == 0, error);
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        private bool ExecuteTexconv(string arguments)
        {
            var result = ExecuteTexconvWithOutput(arguments);
            if (!result.success && !string.IsNullOrEmpty(result.error))
            {
                var errLine = result.error.Trim().Split('\n').FirstOrDefault();
                Debug.WriteLine($"texconv 错误：{errLine}");
            }
            return result.success;
        }

        private void chkDDSGPU_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDDSGPU.Checked)
            {
                RefreshGpuDevices();
                cmbDDSGpu.Enabled = true;
            }
            else
            {
                cmbDDSGpu.Enabled = false;
            }
        }

        private void RefreshGpuDevices()
        {
            _ddsGpuDevices = ListGpuDevices();
            cmbDDSGpu.Items.Clear();

            if (!CheckTexconvExists())
            {
                cmbDDSGpu.Items.Add("未找到 texconv.exe");
                cmbDDSGpu.SelectedIndex = 0;
                cmbDDSGpu.Enabled = false;
                return;
            }

            if (_ddsGpuDevices.Count == 0)
            {
                cmbDDSGpu.Items.Add("未检测到GPU");
                cmbDDSGpu.SelectedIndex = 0;
            }
            else
            {
                foreach (var d in _ddsGpuDevices) cmbDDSGpu.Items.Add(d.NameDisplay ?? d.NameRaw);
                cmbDDSGpu.SelectedIndex = 0;
            }
        }

        private int GetSelectedGpuIndex()
        {
            int sel = cmbDDSGpu.SelectedIndex;
            if (sel >= 0 && sel < _ddsGpuDevices.Count) return _ddsGpuDevices[sel].Index;
            return -1;
        }

        private static string CleanGpuName(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw)) return raw;
            string s = Regex.Replace(raw, @"\[[^\]]*\]", "");
            s = Regex.Replace(s, @"\([^\)]*\)", "");
            s = Regex.Replace(s, @"\b(?:VID|VEN|VENDOR|PID|DEV|DEVICE)[:=]?\s*[0-9A-Fa-fx]+", "", RegexOptions.IgnoreCase);
            s = Regex.Replace(s, @"\s{2,}", " ").Trim();
            return string.IsNullOrEmpty(s) ? raw : s;
        }

        private List<GpuDevice> ListGpuDevices()
        {
            var result = new List<GpuDevice>();
            try
            {
                if (!CheckTexconvExists()) return result;

                var psi = new ProcessStartInfo
                {
                    FileName = DDS_TexconvPath,
                    Arguments = "-gpu",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using (var p = Process.Start(psi))
                {
                    string output = p.StandardOutput.ReadToEnd();
                    p.WaitForExit(5000);

                    foreach (var raw in (output ?? string.Empty).Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        var line = raw.Trim();
                        var m = Regex.Match(line, @"^(?<idx>\d+)\s*:\s*(?<name>.+)$");
                        if (m.Success)
                        {
                            if (int.TryParse(m.Groups["idx"].Value, out int idx))
                            {
                                string nameRaw = m.Groups["name"].Value.Trim();
                                result.Add(new GpuDevice { Index = idx, NameRaw = nameRaw, NameDisplay = CleanGpuName(nameRaw) });
                            }
                        }
                        else
                        {
                            var m2 = Regex.Match(line, @"GPU\s+(?<idx>\d+)\s*:\s*(?<name>.+)", RegexOptions.IgnoreCase);
                            if (m2.Success && int.TryParse(m2.Groups["idx"].Value, out int idx2))
                            {
                                string nameRaw = m2.Groups["name"].Value.Trim();
                                result.Add(new GpuDevice { Index = idx2, NameRaw = nameRaw, NameDisplay = CleanGpuName(nameRaw) });
                            }
                        }
                    }
                }
            }
            catch { }
            return result;
        }

        private void tlpRoot_Paint(object sender, PaintEventArgs e) { }
        private void gbDDS_Enter(object sender, EventArgs e) { }
        private void flpDDSOptions_Paint(object sender, PaintEventArgs e) { }
        private void tlpRoot_Paint_1(object sender, PaintEventArgs e) { }
    }
}
