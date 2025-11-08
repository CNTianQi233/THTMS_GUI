using System;
using System.Windows.Forms;

namespace THTMS_GUI
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }
        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelMain = new System.Windows.Forms.Panel();
            this.tlpRoot = new System.Windows.Forms.TableLayoutPanel();
            this.gbData = new System.Windows.Forms.GroupBox();
            this.tlpData = new System.Windows.Forms.TableLayoutPanel();
            this.lblDataIn = new System.Windows.Forms.Label();
            this.tbDataIn = new System.Windows.Forms.TextBox();
            this.btnDataIn = new System.Windows.Forms.Button();
            this.lblDataOut = new System.Windows.Forms.Label();
            this.tbDataOut = new System.Windows.Forms.TextBox();
            this.btnDataOut = new System.Windows.Forms.Button();
            this.flpDataOptions = new System.Windows.Forms.FlowLayoutPanel();
            this.chkDataDelete = new System.Windows.Forms.CheckBox();
            this.flpDataBtns = new System.Windows.Forms.TableLayoutPanel();
            this.btnDataUnpack = new System.Windows.Forms.Button();
            this.btnDataPack = new System.Windows.Forms.Button();
            this.btnDataList = new System.Windows.Forms.Button();
            this.gbGraphics = new System.Windows.Forms.GroupBox();
            this.tlpGraphics = new System.Windows.Forms.TableLayoutPanel();
            this.lblGraphicsIn = new System.Windows.Forms.Label();
            this.tbGraphicsIn = new System.Windows.Forms.TextBox();
            this.btnGraphicsIn = new System.Windows.Forms.Button();
            this.lblGraphicsOut = new System.Windows.Forms.Label();
            this.tbGraphicsOut = new System.Windows.Forms.TextBox();
            this.btnGraphicsOut = new System.Windows.Forms.Button();
            this.lblKey = new System.Windows.Forms.Label();
            this.cmbGraphicsKey = new System.Windows.Forms.ComboBox();
            this.flpChecks = new System.Windows.Forms.FlowLayoutPanel();
            this.chkGraphicsRecursive = new System.Windows.Forms.CheckBox();
            this.chkGraphicsDelete = new System.Windows.Forms.CheckBox();
            this.flpGBtns = new System.Windows.Forms.TableLayoutPanel();
            this.btnGraphicsUnpack = new System.Windows.Forms.Button();
            this.btnGraphicsPack = new System.Windows.Forms.Button();
            this.btnGraphicsKeyInfo = new System.Windows.Forms.Button();
            this.gbBGM = new System.Windows.Forms.GroupBox();
            this.tlpBGM = new System.Windows.Forms.TableLayoutPanel();
            this.lblBGMIn = new System.Windows.Forms.Label();
            this.tbBGMIn = new System.Windows.Forms.TextBox();
            this.btnBGMIn = new System.Windows.Forms.Button();
            this.lblBGMOut = new System.Windows.Forms.Label();
            this.tbBGMOut = new System.Windows.Forms.TextBox();
            this.btnBGMOut = new System.Windows.Forms.Button();
            this.flpBGMBtns = new System.Windows.Forms.TableLayoutPanel();
            this.btnBGMUnpack = new System.Windows.Forms.Button();
            this.btnBGMPack = new System.Windows.Forms.Button();
            this.gbDDS = new System.Windows.Forms.GroupBox();
            this.tlpDDS = new System.Windows.Forms.TableLayoutPanel();
            this.lblDDSIn = new System.Windows.Forms.Label();
            this.tbDDSIn = new System.Windows.Forms.TextBox();
            this.btnDDSIn = new System.Windows.Forms.Button();
            this.lblDDSOut = new System.Windows.Forms.Label();
            this.tbDDSOut = new System.Windows.Forms.TextBox();
            this.btnDDSOut = new System.Windows.Forms.Button();
            this.lblDDSGpu = new System.Windows.Forms.Label();
            this.cmbDDSGpu = new System.Windows.Forms.ComboBox();
            this.lblDDSImgFormat = new System.Windows.Forms.Label();
            this.cmbDDSImgFormat = new System.Windows.Forms.ComboBox();
            this.btnDDSGetTexconv = new System.Windows.Forms.Button();
            this.flpDDSOptions = new System.Windows.Forms.FlowLayoutPanel();
            this.chkDDSRecursive = new System.Windows.Forms.CheckBox();
            this.chkDDSUseListExclude = new System.Windows.Forms.CheckBox();
            this.chkDDSUseGlistExclude = new System.Windows.Forms.CheckBox();
            this.chkDDSUseDefaultExclude = new System.Windows.Forms.CheckBox();
            this.chkDDSGPU = new System.Windows.Forms.CheckBox();
            this.flpDDSBtns = new System.Windows.Forms.TableLayoutPanel();
            this.btnDDSUnpack = new System.Windows.Forms.Button();
            this.btnDDSPack = new System.Windows.Forms.Button();
            this.panelProgressContainer = new System.Windows.Forms.Panel();
            this.progressBarMain = new System.Windows.Forms.ProgressBar();
            this.btnForceStop = new System.Windows.Forms.Button();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.menuItemDebugTaskbar = new System.Windows.Forms.ToolStripMenuItem();
            this.panelMain.SuspendLayout();
            this.tlpRoot.SuspendLayout();
            this.gbData.SuspendLayout();
            this.tlpData.SuspendLayout();
            this.flpDataOptions.SuspendLayout();
            this.flpDataBtns.SuspendLayout();
            this.gbGraphics.SuspendLayout();
            this.tlpGraphics.SuspendLayout();
            this.flpChecks.SuspendLayout();
            this.flpGBtns.SuspendLayout();
            this.gbBGM.SuspendLayout();
            this.tlpBGM.SuspendLayout();
            this.flpBGMBtns.SuspendLayout();
            this.gbDDS.SuspendLayout();
            this.tlpDDS.SuspendLayout();
            this.flpDDSOptions.SuspendLayout();
            this.flpDDSBtns.SuspendLayout();
            this.panelProgressContainer.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.AutoScroll = true;
            this.panelMain.Controls.Add(this.tlpRoot);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(12);
            this.panelMain.Size = new System.Drawing.Size(644, 1238);
            this.panelMain.TabIndex = 0;
            // 
            // tlpRoot
            // 
            this.tlpRoot.AutoSize = true;
            this.tlpRoot.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpRoot.ColumnCount = 1;
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpRoot.Controls.Add(this.gbData, 0, 0);
            this.tlpRoot.Controls.Add(this.gbGraphics, 0, 1);
            this.tlpRoot.Controls.Add(this.gbBGM, 0, 2);
            this.tlpRoot.Controls.Add(this.gbDDS, 0, 3);
            this.tlpRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRoot.Location = new System.Drawing.Point(12, 12);
            this.tlpRoot.Name = "tlpRoot";
            this.tlpRoot.RowCount = 4;
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpRoot.Size = new System.Drawing.Size(620, 1214);
            this.tlpRoot.TabIndex = 0;
            this.tlpRoot.Paint += new System.Windows.Forms.PaintEventHandler(this.tlpRoot_Paint_1);
            // 
            // gbData
            // 
            this.gbData.AutoSize = true;
            this.gbData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbData.Controls.Add(this.tlpData);
            this.gbData.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbData.Location = new System.Drawing.Point(0, 0);
            this.gbData.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.gbData.Name = "gbData";
            this.gbData.Padding = new System.Windows.Forms.Padding(12);
            this.gbData.Size = new System.Drawing.Size(620, 236);
            this.gbData.TabIndex = 0;
            this.gbData.TabStop = false;
            this.gbData.Text = "Data（图像/Data 通用）";
            // 
            // tlpData
            // 
            this.tlpData.AutoSize = true;
            this.tlpData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpData.ColumnCount = 4;
            this.tlpData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tlpData.Controls.Add(this.lblDataIn, 0, 0);
            this.tlpData.Controls.Add(this.tbDataIn, 1, 0);
            this.tlpData.Controls.Add(this.btnDataIn, 2, 0);
            this.tlpData.Controls.Add(this.lblDataOut, 0, 1);
            this.tlpData.Controls.Add(this.tbDataOut, 1, 1);
            this.tlpData.Controls.Add(this.btnDataOut, 2, 1);
            this.tlpData.Controls.Add(this.flpDataOptions, 0, 2);
            this.tlpData.Controls.Add(this.flpDataBtns, 0, 3);
            this.tlpData.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpData.Location = new System.Drawing.Point(12, 38);
            this.tlpData.MinimumSize = new System.Drawing.Size(596, 186);
            this.tlpData.Name = "tlpData";
            this.tlpData.RowCount = 4;
            this.tlpData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tlpData.Size = new System.Drawing.Size(596, 186);
            this.tlpData.TabIndex = 0;
            // 
            // lblDataIn
            // 
            this.lblDataIn.Location = new System.Drawing.Point(0, 6);
            this.lblDataIn.Margin = new System.Windows.Forms.Padding(0, 6, 8, 6);
            this.lblDataIn.Name = "lblDataIn";
            this.lblDataIn.Size = new System.Drawing.Size(140, 28);
            this.lblDataIn.TabIndex = 0;
            this.lblDataIn.Text = "源路径（多选）";
            this.lblDataIn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbDataIn
            // 
            this.tbDataIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDataIn.BackColor = System.Drawing.Color.White;
            this.tbDataIn.Location = new System.Drawing.Point(150, 4);
            this.tbDataIn.Margin = new System.Windows.Forms.Padding(0, 4, 8, 4);
            this.tbDataIn.Name = "tbDataIn";
            this.tbDataIn.Size = new System.Drawing.Size(382, 33);
            this.tbDataIn.TabIndex = 1;
            // 
            // btnDataIn
            // 
            this.btnDataIn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnDataIn.Location = new System.Drawing.Point(540, 4);
            this.btnDataIn.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.btnDataIn.Name = "btnDataIn";
            this.btnDataIn.Size = new System.Drawing.Size(40, 32);
            this.btnDataIn.TabIndex = 2;
            this.btnDataIn.Text = "...";
            this.btnDataIn.Click += new System.EventHandler(this.btnDataIn_Click);
            // 
            // lblDataOut
            // 
            this.lblDataOut.Location = new System.Drawing.Point(0, 46);
            this.lblDataOut.Margin = new System.Windows.Forms.Padding(0, 6, 8, 6);
            this.lblDataOut.Name = "lblDataOut";
            this.lblDataOut.Size = new System.Drawing.Size(140, 28);
            this.lblDataOut.TabIndex = 3;
            this.lblDataOut.Text = "输出路径";
            this.lblDataOut.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbDataOut
            // 
            this.tbDataOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDataOut.BackColor = System.Drawing.Color.White;
            this.tbDataOut.Location = new System.Drawing.Point(150, 44);
            this.tbDataOut.Margin = new System.Windows.Forms.Padding(0, 4, 8, 4);
            this.tbDataOut.Name = "tbDataOut";
            this.tbDataOut.Size = new System.Drawing.Size(382, 33);
            this.tbDataOut.TabIndex = 4;
            // 
            // btnDataOut
            // 
            this.btnDataOut.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnDataOut.Location = new System.Drawing.Point(540, 44);
            this.btnDataOut.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.btnDataOut.Name = "btnDataOut";
            this.btnDataOut.Size = new System.Drawing.Size(40, 32);
            this.btnDataOut.TabIndex = 5;
            this.btnDataOut.Text = "...";
            this.btnDataOut.Click += new System.EventHandler(this.btnDataOut_Click);
            // 
            // flpDataOptions
            // 
            this.tlpData.SetColumnSpan(this.flpDataOptions, 3);
            this.flpDataOptions.Controls.Add(this.chkDataDelete);
            this.flpDataOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpDataOptions.Location = new System.Drawing.Point(0, 80);
            this.flpDataOptions.Margin = new System.Windows.Forms.Padding(0);
            this.flpDataOptions.Name = "flpDataOptions";
            this.flpDataOptions.Size = new System.Drawing.Size(580, 40);
            this.flpDataOptions.TabIndex = 7;
            // 
            // chkDataDelete
            // 
            this.chkDataDelete.AutoSize = true;
            this.chkDataDelete.Location = new System.Drawing.Point(3, 3);
            this.chkDataDelete.Name = "chkDataDelete";
            this.chkDataDelete.Size = new System.Drawing.Size(138, 31);
            this.chkDataDelete.TabIndex = 0;
            this.chkDataDelete.Text = "处理后删除";
            // 
            // flpDataBtns
            // 
            this.flpDataBtns.ColumnCount = 3;
            this.tlpData.SetColumnSpan(this.flpDataBtns, 3);
            this.flpDataBtns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
            this.flpDataBtns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
            this.flpDataBtns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.334F));
            this.flpDataBtns.Controls.Add(this.btnDataUnpack, 0, 0);
            this.flpDataBtns.Controls.Add(this.btnDataPack, 1, 0);
            this.flpDataBtns.Controls.Add(this.btnDataList, 2, 0);
            this.flpDataBtns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpDataBtns.Location = new System.Drawing.Point(0, 126);
            this.flpDataBtns.Margin = new System.Windows.Forms.Padding(0, 6, 0, 8);
            this.flpDataBtns.Name = "flpDataBtns";
            this.flpDataBtns.RowCount = 1;
            this.flpDataBtns.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.flpDataBtns.Size = new System.Drawing.Size(580, 52);
            this.flpDataBtns.TabIndex = 6;
            // 
            // btnDataUnpack
            // 
            this.btnDataUnpack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDataUnpack.Location = new System.Drawing.Point(0, 0);
            this.btnDataUnpack.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.btnDataUnpack.MinimumSize = new System.Drawing.Size(120, 44);
            this.btnDataUnpack.Name = "btnDataUnpack";
            this.btnDataUnpack.Size = new System.Drawing.Size(187, 52);
            this.btnDataUnpack.TabIndex = 0;
            this.btnDataUnpack.Text = "解包到目标";
            this.btnDataUnpack.Click += new System.EventHandler(this.btnDataUnpack_Click);
            // 
            // btnDataPack
            // 
            this.btnDataPack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDataPack.Location = new System.Drawing.Point(199, 0);
            this.btnDataPack.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.btnDataPack.MinimumSize = new System.Drawing.Size(120, 44);
            this.btnDataPack.Name = "btnDataPack";
            this.btnDataPack.Size = new System.Drawing.Size(181, 52);
            this.btnDataPack.TabIndex = 1;
            this.btnDataPack.Text = "打包";
            this.btnDataPack.Click += new System.EventHandler(this.btnDataPack_Click);
            // 
            // btnDataList
            // 
            this.btnDataList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDataList.Location = new System.Drawing.Point(392, 0);
            this.btnDataList.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnDataList.MinimumSize = new System.Drawing.Size(120, 44);
            this.btnDataList.Name = "btnDataList";
            this.btnDataList.Size = new System.Drawing.Size(188, 52);
            this.btnDataList.TabIndex = 2;
            this.btnDataList.Text = "生成最新 list";
            this.btnDataList.Click += new System.EventHandler(this.btnDataList_Click);
            // 
            // gbGraphics
            // 
            this.gbGraphics.AutoSize = true;
            this.gbGraphics.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbGraphics.Controls.Add(this.tlpGraphics);
            this.gbGraphics.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbGraphics.Location = new System.Drawing.Point(0, 248);
            this.gbGraphics.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.gbGraphics.Name = "gbGraphics";
            this.gbGraphics.Padding = new System.Windows.Forms.Padding(12);
            this.gbGraphics.Size = new System.Drawing.Size(620, 276);
            this.gbGraphics.TabIndex = 1;
            this.gbGraphics.TabStop = false;
            this.gbGraphics.Text = "Graphics（图像加/解密）";
            // 
            // tlpGraphics
            // 
            this.tlpGraphics.AutoSize = true;
            this.tlpGraphics.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpGraphics.ColumnCount = 4;
            this.tlpGraphics.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpGraphics.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpGraphics.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpGraphics.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tlpGraphics.Controls.Add(this.lblGraphicsIn, 0, 0);
            this.tlpGraphics.Controls.Add(this.tbGraphicsIn, 1, 0);
            this.tlpGraphics.Controls.Add(this.btnGraphicsIn, 2, 0);
            this.tlpGraphics.Controls.Add(this.lblGraphicsOut, 0, 1);
            this.tlpGraphics.Controls.Add(this.tbGraphicsOut, 1, 1);
            this.tlpGraphics.Controls.Add(this.btnGraphicsOut, 2, 1);
            this.tlpGraphics.Controls.Add(this.lblKey, 0, 2);
            this.tlpGraphics.Controls.Add(this.cmbGraphicsKey, 1, 2);
            this.tlpGraphics.Controls.Add(this.flpChecks, 0, 3);
            this.tlpGraphics.Controls.Add(this.flpGBtns, 0, 4);
            this.tlpGraphics.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpGraphics.Location = new System.Drawing.Point(12, 38);
            this.tlpGraphics.MinimumSize = new System.Drawing.Size(596, 226);
            this.tlpGraphics.Name = "tlpGraphics";
            this.tlpGraphics.RowCount = 5;
            this.tlpGraphics.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpGraphics.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpGraphics.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpGraphics.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpGraphics.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tlpGraphics.Size = new System.Drawing.Size(596, 226);
            this.tlpGraphics.TabIndex = 0;
            // 
            // lblGraphicsIn
            // 
            this.lblGraphicsIn.Location = new System.Drawing.Point(0, 6);
            this.lblGraphicsIn.Margin = new System.Windows.Forms.Padding(0, 6, 8, 6);
            this.lblGraphicsIn.Name = "lblGraphicsIn";
            this.lblGraphicsIn.Size = new System.Drawing.Size(140, 28);
            this.lblGraphicsIn.TabIndex = 0;
            this.lblGraphicsIn.Text = "源路径";
            this.lblGraphicsIn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbGraphicsIn
            // 
            this.tbGraphicsIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbGraphicsIn.BackColor = System.Drawing.Color.White;
            this.tbGraphicsIn.Location = new System.Drawing.Point(150, 4);
            this.tbGraphicsIn.Margin = new System.Windows.Forms.Padding(0, 4, 8, 4);
            this.tbGraphicsIn.Name = "tbGraphicsIn";
            this.tbGraphicsIn.Size = new System.Drawing.Size(382, 33);
            this.tbGraphicsIn.TabIndex = 1;
            // 
            // btnGraphicsIn
            // 
            this.btnGraphicsIn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnGraphicsIn.Location = new System.Drawing.Point(540, 4);
            this.btnGraphicsIn.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.btnGraphicsIn.Name = "btnGraphicsIn";
            this.btnGraphicsIn.Size = new System.Drawing.Size(40, 32);
            this.btnGraphicsIn.TabIndex = 2;
            this.btnGraphicsIn.Text = "...";
            this.btnGraphicsIn.Click += new System.EventHandler(this.btnGraphicsIn_Click);
            // 
            // lblGraphicsOut
            // 
            this.lblGraphicsOut.Location = new System.Drawing.Point(0, 46);
            this.lblGraphicsOut.Margin = new System.Windows.Forms.Padding(0, 6, 8, 6);
            this.lblGraphicsOut.Name = "lblGraphicsOut";
            this.lblGraphicsOut.Size = new System.Drawing.Size(140, 28);
            this.lblGraphicsOut.TabIndex = 3;
            this.lblGraphicsOut.Text = "输出路径";
            this.lblGraphicsOut.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbGraphicsOut
            // 
            this.tbGraphicsOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbGraphicsOut.BackColor = System.Drawing.Color.White;
            this.tbGraphicsOut.Location = new System.Drawing.Point(150, 44);
            this.tbGraphicsOut.Margin = new System.Windows.Forms.Padding(0, 4, 8, 4);
            this.tbGraphicsOut.Name = "tbGraphicsOut";
            this.tbGraphicsOut.Size = new System.Drawing.Size(382, 33);
            this.tbGraphicsOut.TabIndex = 4;
            // 
            // btnGraphicsOut
            // 
            this.btnGraphicsOut.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnGraphicsOut.Location = new System.Drawing.Point(540, 44);
            this.btnGraphicsOut.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.btnGraphicsOut.Name = "btnGraphicsOut";
            this.btnGraphicsOut.Size = new System.Drawing.Size(40, 32);
            this.btnGraphicsOut.TabIndex = 5;
            this.btnGraphicsOut.Text = "...";
            this.btnGraphicsOut.Click += new System.EventHandler(this.btnGraphicsOut_Click);
            // 
            // lblKey
            // 
            this.lblKey.Location = new System.Drawing.Point(0, 86);
            this.lblKey.Margin = new System.Windows.Forms.Padding(0, 6, 8, 6);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(140, 28);
            this.lblKey.TabIndex = 6;
            this.lblKey.Text = "密钥";
            this.lblKey.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbGraphicsKey
            // 
            this.cmbGraphicsKey.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbGraphicsKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGraphicsKey.Location = new System.Drawing.Point(150, 87);
            this.cmbGraphicsKey.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.cmbGraphicsKey.Name = "cmbGraphicsKey";
            this.cmbGraphicsKey.Size = new System.Drawing.Size(382, 35);
            this.cmbGraphicsKey.TabIndex = 7;
            // 
            // flpChecks
            // 
            this.flpChecks.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.flpChecks.AutoSize = true;
            this.tlpGraphics.SetColumnSpan(this.flpChecks, 3);
            this.flpChecks.Controls.Add(this.chkGraphicsRecursive);
            this.flpChecks.Controls.Add(this.chkGraphicsDelete);
            this.flpChecks.Location = new System.Drawing.Point(0, 123);
            this.flpChecks.Margin = new System.Windows.Forms.Padding(0);
            this.flpChecks.Name = "flpChecks";
            this.flpChecks.Size = new System.Drawing.Size(272, 34);
            this.flpChecks.TabIndex = 8;
            this.flpChecks.WrapContents = false;
            // 
            // chkGraphicsRecursive
            // 
            this.chkGraphicsRecursive.AutoSize = true;
            this.chkGraphicsRecursive.Location = new System.Drawing.Point(0, 3);
            this.chkGraphicsRecursive.Margin = new System.Windows.Forms.Padding(0, 3, 16, 0);
            this.chkGraphicsRecursive.Name = "chkGraphicsRecursive";
            this.chkGraphicsRecursive.Size = new System.Drawing.Size(118, 31);
            this.chkGraphicsRecursive.TabIndex = 0;
            this.chkGraphicsRecursive.Text = "递归模式";
            // 
            // chkGraphicsDelete
            // 
            this.chkGraphicsDelete.AutoSize = true;
            this.chkGraphicsDelete.Location = new System.Drawing.Point(134, 3);
            this.chkGraphicsDelete.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.chkGraphicsDelete.Name = "chkGraphicsDelete";
            this.chkGraphicsDelete.Size = new System.Drawing.Size(138, 31);
            this.chkGraphicsDelete.TabIndex = 1;
            this.chkGraphicsDelete.Text = "处理后删除";
            // 
            // flpGBtns
            // 
            this.flpGBtns.ColumnCount = 3;
            this.tlpGraphics.SetColumnSpan(this.flpGBtns, 3);
            this.flpGBtns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
            this.flpGBtns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
            this.flpGBtns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.334F));
            this.flpGBtns.Controls.Add(this.btnGraphicsUnpack, 0, 0);
            this.flpGBtns.Controls.Add(this.btnGraphicsPack, 1, 0);
            this.flpGBtns.Controls.Add(this.btnGraphicsKeyInfo, 2, 0);
            this.flpGBtns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpGBtns.Location = new System.Drawing.Point(0, 166);
            this.flpGBtns.Margin = new System.Windows.Forms.Padding(0, 6, 0, 8);
            this.flpGBtns.Name = "flpGBtns";
            this.flpGBtns.RowCount = 1;
            this.flpGBtns.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.flpGBtns.Size = new System.Drawing.Size(580, 52);
            this.flpGBtns.TabIndex = 9;
            // 
            // btnGraphicsUnpack
            // 
            this.btnGraphicsUnpack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGraphicsUnpack.Location = new System.Drawing.Point(0, 0);
            this.btnGraphicsUnpack.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.btnGraphicsUnpack.MinimumSize = new System.Drawing.Size(120, 44);
            this.btnGraphicsUnpack.Name = "btnGraphicsUnpack";
            this.btnGraphicsUnpack.Size = new System.Drawing.Size(187, 52);
            this.btnGraphicsUnpack.TabIndex = 0;
            this.btnGraphicsUnpack.Text = "解包";
            this.btnGraphicsUnpack.Click += new System.EventHandler(this.btnGraphicsUnpack_Click);
            // 
            // btnGraphicsPack
            // 
            this.btnGraphicsPack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGraphicsPack.Location = new System.Drawing.Point(199, 0);
            this.btnGraphicsPack.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.btnGraphicsPack.MinimumSize = new System.Drawing.Size(120, 44);
            this.btnGraphicsPack.Name = "btnGraphicsPack";
            this.btnGraphicsPack.Size = new System.Drawing.Size(181, 52);
            this.btnGraphicsPack.TabIndex = 1;
            this.btnGraphicsPack.Text = "打包";
            this.btnGraphicsPack.Click += new System.EventHandler(this.btnGraphicsPack_Click);
            // 
            // btnGraphicsKeyInfo
            // 
            this.btnGraphicsKeyInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGraphicsKeyInfo.Location = new System.Drawing.Point(392, 0);
            this.btnGraphicsKeyInfo.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnGraphicsKeyInfo.MinimumSize = new System.Drawing.Size(120, 44);
            this.btnGraphicsKeyInfo.Name = "btnGraphicsKeyInfo";
            this.btnGraphicsKeyInfo.Size = new System.Drawing.Size(188, 52);
            this.btnGraphicsKeyInfo.TabIndex = 2;
            this.btnGraphicsKeyInfo.Text = "密钥说明";
            this.btnGraphicsKeyInfo.Click += new System.EventHandler(this.btnGraphicsKeyInfo_Click);
            // 
            // gbBGM
            // 
            this.gbBGM.AutoSize = true;
            this.gbBGM.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbBGM.Controls.Add(this.tlpBGM);
            this.gbBGM.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbBGM.Location = new System.Drawing.Point(0, 536);
            this.gbBGM.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.gbBGM.Name = "gbBGM";
            this.gbBGM.Padding = new System.Windows.Forms.Padding(12);
            this.gbBGM.Size = new System.Drawing.Size(620, 196);
            this.gbBGM.TabIndex = 2;
            this.gbBGM.TabStop = false;
            this.gbBGM.Text = "BGM（音频）";
            // 
            // tlpBGM
            // 
            this.tlpBGM.AutoSize = true;
            this.tlpBGM.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpBGM.ColumnCount = 4;
            this.tlpBGM.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpBGM.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBGM.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpBGM.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tlpBGM.Controls.Add(this.lblBGMIn, 0, 0);
            this.tlpBGM.Controls.Add(this.tbBGMIn, 1, 0);
            this.tlpBGM.Controls.Add(this.btnBGMIn, 2, 0);
            this.tlpBGM.Controls.Add(this.lblBGMOut, 0, 1);
            this.tlpBGM.Controls.Add(this.tbBGMOut, 1, 1);
            this.tlpBGM.Controls.Add(this.btnBGMOut, 2, 1);
            this.tlpBGM.Controls.Add(this.flpBGMBtns, 0, 2);
            this.tlpBGM.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpBGM.Location = new System.Drawing.Point(12, 38);
            this.tlpBGM.MinimumSize = new System.Drawing.Size(596, 146);
            this.tlpBGM.Name = "tlpBGM";
            this.tlpBGM.RowCount = 3;
            this.tlpBGM.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpBGM.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpBGM.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tlpBGM.Size = new System.Drawing.Size(596, 146);
            this.tlpBGM.TabIndex = 0;
            // 
            // lblBGMIn
            // 
            this.lblBGMIn.Location = new System.Drawing.Point(0, 6);
            this.lblBGMIn.Margin = new System.Windows.Forms.Padding(0, 6, 8, 6);
            this.lblBGMIn.Name = "lblBGMIn";
            this.lblBGMIn.Size = new System.Drawing.Size(140, 28);
            this.lblBGMIn.TabIndex = 0;
            this.lblBGMIn.Text = "源路径（多选）";
            this.lblBGMIn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbBGMIn
            // 
            this.tbBGMIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBGMIn.BackColor = System.Drawing.Color.White;
            this.tbBGMIn.Location = new System.Drawing.Point(150, 4);
            this.tbBGMIn.Margin = new System.Windows.Forms.Padding(0, 4, 8, 4);
            this.tbBGMIn.Name = "tbBGMIn";
            this.tbBGMIn.Size = new System.Drawing.Size(382, 33);
            this.tbBGMIn.TabIndex = 1;
            // 
            // btnBGMIn
            // 
            this.btnBGMIn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnBGMIn.Location = new System.Drawing.Point(540, 4);
            this.btnBGMIn.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.btnBGMIn.Name = "btnBGMIn";
            this.btnBGMIn.Size = new System.Drawing.Size(40, 32);
            this.btnBGMIn.TabIndex = 2;
            this.btnBGMIn.Text = "...";
            this.btnBGMIn.Click += new System.EventHandler(this.btnBGMIn_Click);
            // 
            // lblBGMOut
            // 
            this.lblBGMOut.Location = new System.Drawing.Point(0, 46);
            this.lblBGMOut.Margin = new System.Windows.Forms.Padding(0, 6, 8, 6);
            this.lblBGMOut.Name = "lblBGMOut";
            this.lblBGMOut.Size = new System.Drawing.Size(140, 28);
            this.lblBGMOut.TabIndex = 3;
            this.lblBGMOut.Text = "输出路径";
            this.lblBGMOut.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbBGMOut
            // 
            this.tbBGMOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBGMOut.BackColor = System.Drawing.Color.White;
            this.tbBGMOut.Location = new System.Drawing.Point(150, 44);
            this.tbBGMOut.Margin = new System.Windows.Forms.Padding(0, 4, 8, 4);
            this.tbBGMOut.Name = "tbBGMOut";
            this.tbBGMOut.Size = new System.Drawing.Size(382, 33);
            this.tbBGMOut.TabIndex = 4;
            // 
            // btnBGMOut
            // 
            this.btnBGMOut.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnBGMOut.Location = new System.Drawing.Point(540, 44);
            this.btnBGMOut.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.btnBGMOut.Name = "btnBGMOut";
            this.btnBGMOut.Size = new System.Drawing.Size(40, 32);
            this.btnBGMOut.TabIndex = 5;
            this.btnBGMOut.Text = "...";
            this.btnBGMOut.Click += new System.EventHandler(this.btnBGMOut_Click);
            // 
            // flpBGMBtns
            // 
            this.flpBGMBtns.ColumnCount = 2;
            this.tlpBGM.SetColumnSpan(this.flpBGMBtns, 3);
            this.flpBGMBtns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.flpBGMBtns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.flpBGMBtns.Controls.Add(this.btnBGMUnpack, 0, 0);
            this.flpBGMBtns.Controls.Add(this.btnBGMPack, 1, 0);
            this.flpBGMBtns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpBGMBtns.Location = new System.Drawing.Point(0, 86);
            this.flpBGMBtns.Margin = new System.Windows.Forms.Padding(0, 6, 0, 8);
            this.flpBGMBtns.Name = "flpBGMBtns";
            this.flpBGMBtns.RowCount = 1;
            this.flpBGMBtns.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.flpBGMBtns.Size = new System.Drawing.Size(580, 52);
            this.flpBGMBtns.TabIndex = 6;
            // 
            // btnBGMUnpack
            // 
            this.btnBGMUnpack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBGMUnpack.Location = new System.Drawing.Point(0, 0);
            this.btnBGMUnpack.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.btnBGMUnpack.MinimumSize = new System.Drawing.Size(120, 44);
            this.btnBGMUnpack.Name = "btnBGMUnpack";
            this.btnBGMUnpack.Size = new System.Drawing.Size(284, 52);
            this.btnBGMUnpack.TabIndex = 0;
            this.btnBGMUnpack.Text = "解包";
            this.btnBGMUnpack.Click += new System.EventHandler(this.btnBGMUnpack_Click);
            // 
            // btnBGMPack
            // 
            this.btnBGMPack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBGMPack.Location = new System.Drawing.Point(296, 0);
            this.btnBGMPack.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnBGMPack.MinimumSize = new System.Drawing.Size(120, 44);
            this.btnBGMPack.Name = "btnBGMPack";
            this.btnBGMPack.Size = new System.Drawing.Size(284, 52);
            this.btnBGMPack.TabIndex = 1;
            this.btnBGMPack.Text = "打包";
            this.btnBGMPack.Click += new System.EventHandler(this.btnBGMPack_Click);
            // 
            // gbDDS
            // 
            this.gbDDS.AutoSize = true;
            this.gbDDS.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbDDS.Controls.Add(this.tlpDDS);
            this.gbDDS.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbDDS.Location = new System.Drawing.Point(0, 744);
            this.gbDDS.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.gbDDS.Name = "gbDDS";
            this.gbDDS.Padding = new System.Windows.Forms.Padding(20);
            this.gbDDS.Size = new System.Drawing.Size(620, 351);
            this.gbDDS.TabIndex = 3;
            this.gbDDS.TabStop = false;
            this.gbDDS.Text = "DDS（互转）";
            this.gbDDS.Enter += new System.EventHandler(this.gbDDS_Enter);
            // 
            // tlpDDS
            // 
            this.tlpDDS.AutoSize = true;
            this.tlpDDS.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpDDS.ColumnCount = 4;
            this.tlpDDS.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tlpDDS.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDDS.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpDDS.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpDDS.Controls.Add(this.lblDDSIn, 0, 0);
            this.tlpDDS.Controls.Add(this.tbDDSIn, 1, 0);
            this.tlpDDS.Controls.Add(this.btnDDSIn, 2, 0);
            this.tlpDDS.Controls.Add(this.lblDDSOut, 0, 1);
            this.tlpDDS.Controls.Add(this.tbDDSOut, 1, 1);
            this.tlpDDS.Controls.Add(this.btnDDSOut, 2, 1);
            this.tlpDDS.Controls.Add(this.lblDDSGpu, 0, 2);
            this.tlpDDS.Controls.Add(this.cmbDDSGpu, 1, 2);
            this.tlpDDS.Controls.Add(this.lblDDSImgFormat, 0, 3);
            this.tlpDDS.Controls.Add(this.cmbDDSImgFormat, 1, 3);
            this.tlpDDS.Controls.Add(this.btnDDSGetTexconv, 2, 3);
            this.tlpDDS.Controls.Add(this.flpDDSOptions, 0, 4);
            this.tlpDDS.Controls.Add(this.flpDDSBtns, 0, 5);
            this.tlpDDS.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpDDS.Location = new System.Drawing.Point(20, 46);
            this.tlpDDS.MinimumSize = new System.Drawing.Size(580, 285);
            this.tlpDDS.Name = "tlpDDS";
            this.tlpDDS.RowCount = 6;
            this.tlpDDS.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpDDS.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpDDS.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpDDS.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpDDS.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 67F));
            this.tlpDDS.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tlpDDS.Size = new System.Drawing.Size(580, 285);
            this.tlpDDS.TabIndex = 0;
            // 
            // lblDDSIn
            // 
            this.lblDDSIn.Location = new System.Drawing.Point(0, 6);
            this.lblDDSIn.Margin = new System.Windows.Forms.Padding(0, 6, 8, 6);
            this.lblDDSIn.Name = "lblDDSIn";
            this.lblDDSIn.Size = new System.Drawing.Size(140, 28);
            this.lblDDSIn.TabIndex = 0;
            this.lblDDSIn.Text = "源路径";
            this.lblDDSIn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbDDSIn
            // 
            this.tbDDSIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDDSIn.BackColor = System.Drawing.Color.White;
            this.tbDDSIn.Location = new System.Drawing.Point(160, 4);
            this.tbDDSIn.Margin = new System.Windows.Forms.Padding(0, 4, 8, 4);
            this.tbDDSIn.Name = "tbDDSIn";
            this.tbDDSIn.Size = new System.Drawing.Size(332, 33);
            this.tbDDSIn.TabIndex = 1;
            // 
            // btnDDSIn
            // 
            this.btnDDSIn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnDDSIn.Location = new System.Drawing.Point(500, 4);
            this.btnDDSIn.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.btnDDSIn.Name = "btnDDSIn";
            this.btnDDSIn.Size = new System.Drawing.Size(40, 32);
            this.btnDDSIn.TabIndex = 2;
            this.btnDDSIn.Text = "...";
            this.btnDDSIn.Click += new System.EventHandler(this.btnDDSIn_Click);
            // 
            // lblDDSOut
            // 
            this.lblDDSOut.Location = new System.Drawing.Point(0, 46);
            this.lblDDSOut.Margin = new System.Windows.Forms.Padding(0, 6, 8, 6);
            this.lblDDSOut.Name = "lblDDSOut";
            this.lblDDSOut.Size = new System.Drawing.Size(140, 28);
            this.lblDDSOut.TabIndex = 3;
            this.lblDDSOut.Text = "输出路径";
            this.lblDDSOut.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbDDSOut
            // 
            this.tbDDSOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDDSOut.BackColor = System.Drawing.Color.White;
            this.tbDDSOut.Location = new System.Drawing.Point(160, 44);
            this.tbDDSOut.Margin = new System.Windows.Forms.Padding(0, 4, 8, 4);
            this.tbDDSOut.Name = "tbDDSOut";
            this.tbDDSOut.Size = new System.Drawing.Size(332, 33);
            this.tbDDSOut.TabIndex = 4;
            // 
            // btnDDSOut
            // 
            this.btnDDSOut.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnDDSOut.Location = new System.Drawing.Point(500, 44);
            this.btnDDSOut.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.btnDDSOut.Name = "btnDDSOut";
            this.btnDDSOut.Size = new System.Drawing.Size(40, 32);
            this.btnDDSOut.TabIndex = 5;
            this.btnDDSOut.Text = "...";
            this.btnDDSOut.Click += new System.EventHandler(this.btnDDSOut_Click);
            // 
            // lblDDSGpu
            // 
            this.lblDDSGpu.Location = new System.Drawing.Point(0, 86);
            this.lblDDSGpu.Margin = new System.Windows.Forms.Padding(0, 6, 8, 6);
            this.lblDDSGpu.Name = "lblDDSGpu";
            this.lblDDSGpu.Size = new System.Drawing.Size(140, 28);
            this.lblDDSGpu.TabIndex = 15;
            this.lblDDSGpu.Text = "使用的GPU";
            this.lblDDSGpu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbDDSGpu
            // 
            this.cmbDDSGpu.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbDDSGpu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDDSGpu.Location = new System.Drawing.Point(160, 82);
            this.cmbDDSGpu.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.cmbDDSGpu.Name = "cmbDDSGpu";
            this.cmbDDSGpu.Size = new System.Drawing.Size(340, 35);
            this.cmbDDSGpu.TabIndex = 16;
            // 
            // lblDDSImgFormat
            // 
            this.lblDDSImgFormat.Location = new System.Drawing.Point(0, 126);
            this.lblDDSImgFormat.Margin = new System.Windows.Forms.Padding(0, 6, 8, 6);
            this.lblDDSImgFormat.Name = "lblDDSImgFormat";
            this.lblDDSImgFormat.Size = new System.Drawing.Size(140, 28);
            this.lblDDSImgFormat.TabIndex = 11;
            this.lblDDSImgFormat.Text = "解包输出格式";
            this.lblDDSImgFormat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbDDSImgFormat
            // 
            this.cmbDDSImgFormat.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbDDSImgFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDDSImgFormat.Location = new System.Drawing.Point(160, 122);
            this.cmbDDSImgFormat.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.cmbDDSImgFormat.Name = "cmbDDSImgFormat";
            this.cmbDDSImgFormat.Size = new System.Drawing.Size(340, 35);
            this.cmbDDSImgFormat.TabIndex = 12;
            // 
            // btnDDSGetTexconv
            // 
            this.btnDDSGetTexconv.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnDDSGetTexconv.Location = new System.Drawing.Point(500, 124);
            this.btnDDSGetTexconv.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.btnDDSGetTexconv.Name = "btnDDSGetTexconv";
            this.btnDDSGetTexconv.Size = new System.Drawing.Size(40, 32);
            this.btnDDSGetTexconv.TabIndex = 13;
            this.btnDDSGetTexconv.Text = "↧";
            this.btnDDSGetTexconv.Click += new System.EventHandler(this.btnDDSGetTexconv_Click);
            // 
            // flpDDSOptions
            // 
            this.flpDDSOptions.AutoSize = true;
            this.flpDDSOptions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpDDS.SetColumnSpan(this.flpDDSOptions, 3);
            this.flpDDSOptions.Controls.Add(this.chkDDSRecursive);
            this.flpDDSOptions.Controls.Add(this.chkDDSUseListExclude);
            this.flpDDSOptions.Controls.Add(this.chkDDSUseGlistExclude);
            this.flpDDSOptions.Controls.Add(this.chkDDSUseDefaultExclude);
            this.flpDDSOptions.Controls.Add(this.chkDDSGPU);
            this.flpDDSOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpDDSOptions.Location = new System.Drawing.Point(0, 160);
            this.flpDDSOptions.Margin = new System.Windows.Forms.Padding(0);
            this.flpDDSOptions.Name = "flpDDSOptions";
            this.flpDDSOptions.Size = new System.Drawing.Size(540, 67);
            this.flpDDSOptions.TabIndex = 10;
            this.flpDDSOptions.Paint += new System.Windows.Forms.PaintEventHandler(this.flpDDSOptions_Paint);
            // 
            // chkDDSRecursive
            // 
            this.chkDDSRecursive.AutoSize = true;
            this.chkDDSRecursive.Location = new System.Drawing.Point(3, 3);
            this.chkDDSRecursive.Name = "chkDDSRecursive";
            this.chkDDSRecursive.Size = new System.Drawing.Size(118, 31);
            this.chkDDSRecursive.TabIndex = 0;
            this.chkDDSRecursive.Text = "递归模式";
            // 
            // chkDDSUseListExclude
            // 
            this.chkDDSUseListExclude.AutoSize = true;
            this.chkDDSUseListExclude.Location = new System.Drawing.Point(127, 3);
            this.chkDDSUseListExclude.Name = "chkDDSUseListExclude";
            this.chkDDSUseListExclude.Size = new System.Drawing.Size(196, 31);
            this.chkDDSUseListExclude.TabIndex = 1;
            this.chkDDSUseListExclude.Text = "使用 list 作为排除";
            // 
            // chkDDSUseGlistExclude
            // 
            this.chkDDSUseGlistExclude.AutoSize = true;
            this.chkDDSUseGlistExclude.Location = new System.Drawing.Point(3, 40);
            this.chkDDSUseGlistExclude.Name = "chkDDSUseGlistExclude";
            this.chkDDSUseGlistExclude.Size = new System.Drawing.Size(209, 31);
            this.chkDDSUseGlistExclude.TabIndex = 2;
            this.chkDDSUseGlistExclude.Text = "使用 glist 作为排除";
            // 
            // chkDDSUseDefaultExclude
            // 
            this.chkDDSUseDefaultExclude.AutoSize = true;
            this.chkDDSUseDefaultExclude.Location = new System.Drawing.Point(218, 40);
            this.chkDDSUseDefaultExclude.Name = "chkDDSUseDefaultExclude";
            this.chkDDSUseDefaultExclude.Size = new System.Drawing.Size(178, 31);
            this.chkDDSUseDefaultExclude.TabIndex = 3;
            this.chkDDSUseDefaultExclude.Text = "使用默认排除表";
            // 
            // chkDDSGPU
            // 
            this.chkDDSGPU.AutoSize = true;
            this.chkDDSGPU.Location = new System.Drawing.Point(402, 40);
            this.chkDDSGPU.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.chkDDSGPU.Name = "chkDDSGPU";
            this.chkDDSGPU.Size = new System.Drawing.Size(126, 31);
            this.chkDDSGPU.TabIndex = 5;
            this.chkDDSGPU.Text = "启用 GPU";
            // 
            // flpDDSBtns
            // 
            this.flpDDSBtns.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpDDSBtns.ColumnCount = 2;
            this.tlpDDS.SetColumnSpan(this.flpDDSBtns, 3);
            this.flpDDSBtns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.flpDDSBtns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.flpDDSBtns.Controls.Add(this.btnDDSUnpack, 0, 0);
            this.flpDDSBtns.Controls.Add(this.btnDDSPack, 1, 0);
            this.flpDDSBtns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpDDSBtns.Location = new System.Drawing.Point(0, 233);
            this.flpDDSBtns.Margin = new System.Windows.Forms.Padding(0, 6, 0, 8);
            this.flpDDSBtns.MinimumSize = new System.Drawing.Size(580, 44);
            this.flpDDSBtns.Name = "flpDDSBtns";
            this.flpDDSBtns.RowCount = 1;
            this.flpDDSBtns.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.flpDDSBtns.Size = new System.Drawing.Size(580, 44);
            this.flpDDSBtns.TabIndex = 14;
            // 
            // btnDDSUnpack
            // 
            this.btnDDSUnpack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDDSUnpack.Location = new System.Drawing.Point(0, 0);
            this.btnDDSUnpack.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.btnDDSUnpack.MinimumSize = new System.Drawing.Size(120, 44);
            this.btnDDSUnpack.Name = "btnDDSUnpack";
            this.btnDDSUnpack.Size = new System.Drawing.Size(284, 44);
            this.btnDDSUnpack.TabIndex = 0;
            this.btnDDSUnpack.Text = "DDS → 图片";
            this.btnDDSUnpack.Click += new System.EventHandler(this.btnDDSUnpack_Click);
            // 
            // btnDDSPack
            // 
            this.btnDDSPack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDDSPack.Location = new System.Drawing.Point(296, 0);
            this.btnDDSPack.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnDDSPack.MinimumSize = new System.Drawing.Size(120, 44);
            this.btnDDSPack.Name = "btnDDSPack";
            this.btnDDSPack.Size = new System.Drawing.Size(284, 44);
            this.btnDDSPack.TabIndex = 1;
            this.btnDDSPack.Text = "图片 → DDS";
            this.btnDDSPack.Click += new System.EventHandler(this.btnDDSPack_Click);
            // 
            // panelProgressContainer
            // 
            this.panelProgressContainer.Controls.Add(this.progressBarMain);
            this.panelProgressContainer.Controls.Add(this.btnForceStop);
            this.panelProgressContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelProgressContainer.Location = new System.Drawing.Point(0, 1238);
            this.panelProgressContainer.Name = "panelProgressContainer";
            this.panelProgressContainer.Size = new System.Drawing.Size(644, 60);
            this.panelProgressContainer.TabIndex = 2;
            // 
            // progressBarMain
            // 
            this.progressBarMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBarMain.Location = new System.Drawing.Point(0, 0);
            this.progressBarMain.MarqueeAnimationSpeed = 10;
            this.progressBarMain.Name = "progressBarMain";
            this.progressBarMain.Size = new System.Drawing.Size(644, 17);
            this.progressBarMain.TabIndex = 0;
            // 
            // btnForceStop
            // 
            this.btnForceStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnForceStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnForceStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnForceStop.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnForceStop.ForeColor = System.Drawing.Color.White;
            this.btnForceStop.Location = new System.Drawing.Point(0, 0);
            this.btnForceStop.Name = "btnForceStop";
            this.btnForceStop.Size = new System.Drawing.Size(644, 60);
            this.btnForceStop.TabIndex = 1;
            this.btnForceStop.Text = "强制停止";
            this.btnForceStop.UseVisualStyleBackColor = false;
            this.btnForceStop.Visible = false;
            this.btnForceStop.Click += new System.EventHandler(this.btnForceStop_Click);
            // 
            // menuStripMain
            // 
            this.menuStripMain.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemDebugTaskbar});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(644, 32);
            this.menuStripMain.TabIndex = 3;
            this.menuStripMain.Text = "menuStripMain";
            // 
            // menuItemDebugTaskbar
            // 
            this.menuItemDebugTaskbar.Name = "menuItemDebugTaskbar";
            this.menuItemDebugTaskbar.Size = new System.Drawing.Size(189, 28);
            this.menuItemDebugTaskbar.Text = "任务栏进度调试(&T)…";
            //this.menuItemDebugTaskbar.Click += new System.EventHandler(this.menuItemDebugTaskbar_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(644, 1298);
            this.Controls.Add(this.menuStripMain);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelProgressContainer);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStripMain;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "THTMSR_GUI";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.tlpRoot.ResumeLayout(false);
            this.tlpRoot.PerformLayout();
            this.gbData.ResumeLayout(false);
            this.gbData.PerformLayout();
            this.tlpData.ResumeLayout(false);
            this.tlpData.PerformLayout();
            this.flpDataOptions.ResumeLayout(false);
            this.flpDataOptions.PerformLayout();
            this.flpDataBtns.ResumeLayout(false);
            this.gbGraphics.ResumeLayout(false);
            this.gbGraphics.PerformLayout();
            this.tlpGraphics.ResumeLayout(false);
            this.tlpGraphics.PerformLayout();
            this.flpChecks.ResumeLayout(false);
            this.flpChecks.PerformLayout();
            this.flpGBtns.ResumeLayout(false);
            this.gbBGM.ResumeLayout(false);
            this.gbBGM.PerformLayout();
            this.tlpBGM.ResumeLayout(false);
            this.tlpBGM.PerformLayout();
            this.flpBGMBtns.ResumeLayout(false);
            this.gbDDS.ResumeLayout(false);
            this.gbDDS.PerformLayout();
            this.tlpDDS.ResumeLayout(false);
            this.tlpDDS.PerformLayout();
            this.flpDDSOptions.ResumeLayout(false);
            this.flpDDSOptions.PerformLayout();
            this.flpDDSBtns.ResumeLayout(false);
            this.panelProgressContainer.ResumeLayout(false);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.TableLayoutPanel tlpRoot;

        private System.Windows.Forms.GroupBox gbData;
        private System.Windows.Forms.TableLayoutPanel tlpData;
        private System.Windows.Forms.Label lblDataIn;
        private System.Windows.Forms.Label lblDataOut;
        private System.Windows.Forms.TextBox tbDataIn;
        private System.Windows.Forms.TextBox tbDataOut;
        private System.Windows.Forms.Button btnDataIn;
        private System.Windows.Forms.Button btnDataOut;
        private System.Windows.Forms.FlowLayoutPanel flpDataOptions;
        private System.Windows.Forms.CheckBox chkDataDelete;
        private System.Windows.Forms.TableLayoutPanel flpDataBtns;
        private System.Windows.Forms.Button btnDataUnpack;
        private System.Windows.Forms.Button btnDataPack;
        private System.Windows.Forms.Button btnDataList;

        private System.Windows.Forms.GroupBox gbGraphics;
        private System.Windows.Forms.TableLayoutPanel tlpGraphics;
        private System.Windows.Forms.Label lblGraphicsIn;
        private System.Windows.Forms.Label lblGraphicsOut;
        private System.Windows.Forms.TextBox tbGraphicsIn;
        private System.Windows.Forms.TextBox tbGraphicsOut;
        private System.Windows.Forms.Button btnGraphicsIn;
        private System.Windows.Forms.Button btnGraphicsOut;
        private System.Windows.Forms.Label lblKey;
        private System.Windows.Forms.ComboBox cmbGraphicsKey;
        private System.Windows.Forms.CheckBox chkGraphicsRecursive;
        private System.Windows.Forms.CheckBox chkGraphicsDelete;
        private System.Windows.Forms.TableLayoutPanel flpGBtns;
        private System.Windows.Forms.Button btnGraphicsUnpack;
        private System.Windows.Forms.Button btnGraphicsPack;
        private System.Windows.Forms.Button btnGraphicsKeyInfo;

        private System.Windows.Forms.GroupBox gbBGM;
        private System.Windows.Forms.TableLayoutPanel tlpBGM;
        private System.Windows.Forms.Label lblBGMIn;
        private System.Windows.Forms.Label lblBGMOut;
        private System.Windows.Forms.TextBox tbBGMIn;
        private System.Windows.Forms.TextBox tbBGMOut;
        private System.Windows.Forms.Button btnBGMIn;
        private System.Windows.Forms.Button btnBGMOut;
        private System.Windows.Forms.TableLayoutPanel flpBGMBtns;
        private System.Windows.Forms.Button btnBGMUnpack;
        private System.Windows.Forms.Button btnBGMPack;

        private System.Windows.Forms.GroupBox gbDDS;
        private System.Windows.Forms.TableLayoutPanel tlpDDS;
        private System.Windows.Forms.Label lblDDSIn;
        private System.Windows.Forms.Label lblDDSOut;
        private System.Windows.Forms.TextBox tbDDSIn;
        private System.Windows.Forms.TextBox tbDDSOut;
        private System.Windows.Forms.Button btnDDSIn;
        private System.Windows.Forms.Button btnDDSOut;
        private System.Windows.Forms.FlowLayoutPanel flpDDSOptions;
        private System.Windows.Forms.CheckBox chkDDSRecursive;
        private System.Windows.Forms.CheckBox chkDDSUseListExclude;
        private System.Windows.Forms.CheckBox chkDDSUseGlistExclude;
        private System.Windows.Forms.CheckBox chkDDSUseDefaultExclude;
        private System.Windows.Forms.CheckBox chkDDSGPU;
        private System.Windows.Forms.Label lblDDSImgFormat;
        private System.Windows.Forms.ComboBox cmbDDSImgFormat;
        private System.Windows.Forms.Button btnDDSGetTexconv;
        private System.Windows.Forms.TableLayoutPanel flpDDSBtns;
        private System.Windows.Forms.Button btnDDSUnpack;
        private System.Windows.Forms.Button btnDDSPack;
        private System.Windows.Forms.Label lblDDSGpu;
        private System.Windows.Forms.ComboBox cmbDDSGpu;

        private System.Windows.Forms.FlowLayoutPanel flpChecks;
        private System.Windows.Forms.Panel panelProgressContainer;
        private System.Windows.Forms.ProgressBar progressBarMain;
        private System.Windows.Forms.Button btnForceStop;

        // ===== 新增字段：菜单 =====
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem menuItemDebugTaskbar;
        // ==========================
    }
}
