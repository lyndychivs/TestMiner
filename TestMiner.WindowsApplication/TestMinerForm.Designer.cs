namespace TestMiner.WindowsApplication
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal partial class TestMinerForm : Form
    {
        private RichTextBox _rtbLogs;
        private TextBox _tbConnectionString;
        private Button _btnSaveConnectionString;
        private OpenFileDialog _openFileDialog;
        private Button _btnSelectFilesToMine;
        private ListBox _listBoxFilesSelected;
        private Button _btnClearListBoxFilesSelected;
        private Button _btnMine;
        private Label _lblConnectionStringStatus;
        private Label _lblConnectionString;
        private Button _btnVerifyConnectionString;
        private Label _lblSelectedFiles;
        private Label _lblLogs;
        private LinkLabel _lblGithub;

        private void InitializeComponent()
        {
            var resources = new ComponentResourceManager(typeof(TestMinerForm));
            _rtbLogs = new RichTextBox();
            _tbConnectionString = new TextBox();
            _btnSaveConnectionString = new Button();
            _openFileDialog = new OpenFileDialog();
            _btnSelectFilesToMine = new Button();
            _listBoxFilesSelected = new ListBox();
            _btnClearListBoxFilesSelected = new Button();
            _btnMine = new Button();
            _lblConnectionStringStatus = new Label();
            _lblConnectionString = new Label();
            _btnVerifyConnectionString = new Button();
            _lblSelectedFiles = new Label();
            _lblLogs = new Label();
            _lblGithub = new LinkLabel();
            _btnClearLogs = new Button();
            SuspendLayout();
            // 
            // _rtbLogs
            // 
            _rtbLogs.Location = new Point(12, 274);
            _rtbLogs.Name = "_rtbLogs";
            _rtbLogs.Size = new Size(956, 387);
            _rtbLogs.TabIndex = 0;
            _rtbLogs.Text = "";
            // 
            // _tbConnectionString
            // 
            _tbConnectionString.Location = new Point(12, 27);
            _tbConnectionString.Name = "_tbConnectionString";
            _tbConnectionString.Size = new Size(956, 23);
            _tbConnectionString.TabIndex = 1;
            // 
            // _btnSaveConnectionString
            // 
            _btnSaveConnectionString.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _btnSaveConnectionString.Location = new Point(792, 56);
            _btnSaveConnectionString.Name = "_btnSaveConnectionString";
            _btnSaveConnectionString.Size = new Size(176, 33);
            _btnSaveConnectionString.TabIndex = 2;
            _btnSaveConnectionString.Text = "Save";
            _btnSaveConnectionString.UseVisualStyleBackColor = true;
            _btnSaveConnectionString.Click += BtnSaveConnectionString_Click;
            // 
            // _openFileDialog
            // 
            _openFileDialog.FileName = "_openFileDialog";
            _openFileDialog.Multiselect = true;
            // 
            // _btnSelectFilesToMine
            // 
            _btnSelectFilesToMine.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _btnSelectFilesToMine.Location = new Point(12, 225);
            _btnSelectFilesToMine.Name = "_btnSelectFilesToMine";
            _btnSelectFilesToMine.Size = new Size(176, 28);
            _btnSelectFilesToMine.TabIndex = 5;
            _btnSelectFilesToMine.Text = "Select File(s)";
            _btnSelectFilesToMine.UseVisualStyleBackColor = true;
            _btnSelectFilesToMine.Click += BtnSelectFilesToMine_Click;
            // 
            // _listBoxFilesSelected
            // 
            _listBoxFilesSelected.FormattingEnabled = true;
            _listBoxFilesSelected.ItemHeight = 15;
            _listBoxFilesSelected.Location = new Point(12, 110);
            _listBoxFilesSelected.Name = "_listBoxFilesSelected";
            _listBoxFilesSelected.Size = new Size(956, 109);
            _listBoxFilesSelected.TabIndex = 6;
            // 
            // _btnClearListBoxFilesSelected
            // 
            _btnClearListBoxFilesSelected.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _btnClearListBoxFilesSelected.Location = new Point(194, 225);
            _btnClearListBoxFilesSelected.Name = "_btnClearListBoxFilesSelected";
            _btnClearListBoxFilesSelected.Size = new Size(176, 28);
            _btnClearListBoxFilesSelected.TabIndex = 7;
            _btnClearListBoxFilesSelected.Text = "Clear";
            _btnClearListBoxFilesSelected.UseVisualStyleBackColor = true;
            _btnClearListBoxFilesSelected.Click += BtnClearListBoxFilesSelected_Click;
            // 
            // _btnMine
            // 
            _btnMine.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _btnMine.Location = new Point(792, 225);
            _btnMine.Name = "_btnMine";
            _btnMine.Size = new Size(176, 28);
            _btnMine.TabIndex = 8;
            _btnMine.Text = "Mine";
            _btnMine.UseVisualStyleBackColor = true;
            _btnMine.Click += BtnMine_Click;
            // 
            // _lblConnectionStringStatus
            // 
            _lblConnectionStringStatus.AutoSize = true;
            _lblConnectionStringStatus.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            _lblConnectionStringStatus.Location = new Point(815, 9);
            _lblConnectionStringStatus.Name = "_lblConnectionStringStatus";
            _lblConnectionStringStatus.Size = new Size(151, 15);
            _lblConnectionStringStatus.TabIndex = 9;
            _lblConnectionStringStatus.Text = "Connection String Status: ...";
            _lblConnectionStringStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _lblConnectionString
            // 
            _lblConnectionString.AutoSize = true;
            _lblConnectionString.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _lblConnectionString.Location = new Point(12, 9);
            _lblConnectionString.Name = "_lblConnectionString";
            _lblConnectionString.Size = new Size(110, 15);
            _lblConnectionString.TabIndex = 10;
            _lblConnectionString.Text = "Connection String:";
            // 
            // _btnVerifyConnectionString
            // 
            _btnVerifyConnectionString.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _btnVerifyConnectionString.Location = new Point(610, 56);
            _btnVerifyConnectionString.Name = "_btnVerifyConnectionString";
            _btnVerifyConnectionString.Size = new Size(176, 33);
            _btnVerifyConnectionString.TabIndex = 11;
            _btnVerifyConnectionString.Text = "Verify";
            _btnVerifyConnectionString.UseVisualStyleBackColor = true;
            _btnVerifyConnectionString.Click += BtnVerifyConnectionString_Click;
            // 
            // _lblSelectedFiles
            // 
            _lblSelectedFiles.AutoSize = true;
            _lblSelectedFiles.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _lblSelectedFiles.Location = new Point(12, 92);
            _lblSelectedFiles.Name = "_lblSelectedFiles";
            _lblSelectedFiles.Size = new Size(86, 15);
            _lblSelectedFiles.TabIndex = 12;
            _lblSelectedFiles.Text = "Selected Files:";
            // 
            // _lblLogs
            // 
            _lblLogs.AutoSize = true;
            _lblLogs.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            _lblLogs.Location = new Point(12, 256);
            _lblLogs.Name = "_lblLogs";
            _lblLogs.Size = new Size(34, 15);
            _lblLogs.TabIndex = 13;
            _lblLogs.Text = "Logs:";
            // 
            // _lblGithub
            // 
            _lblGithub.AutoSize = true;
            _lblGithub.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            _lblGithub.Location = new Point(755, 683);
            _lblGithub.Name = "_lblGithub";
            _lblGithub.Size = new Size(211, 15);
            _lblGithub.TabIndex = 14;
            _lblGithub.TabStop = true;
            _lblGithub.Text = "GitHub - lyndychivs - Test Miner - 2025";
            _lblGithub.LinkClicked += LblGithub_LinkClicked;
            // 
            // _btnClearLogs
            // 
            _btnClearLogs.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _btnClearLogs.Location = new Point(12, 667);
            _btnClearLogs.Name = "_btnClearLogs";
            _btnClearLogs.Size = new Size(176, 28);
            _btnClearLogs.TabIndex = 16;
            _btnClearLogs.Text = "Clear";
            _btnClearLogs.UseVisualStyleBackColor = true;
            _btnClearLogs.Click += BtnClearLogs_Click;
            // 
            // TestMinerForm
            // 
            ClientSize = new Size(980, 707);
            Controls.Add(_btnClearLogs);
            Controls.Add(_lblGithub);
            Controls.Add(_lblLogs);
            Controls.Add(_lblSelectedFiles);
            Controls.Add(_btnVerifyConnectionString);
            Controls.Add(_lblConnectionString);
            Controls.Add(_lblConnectionStringStatus);
            Controls.Add(_btnMine);
            Controls.Add(_btnClearListBoxFilesSelected);
            Controls.Add(_listBoxFilesSelected);
            Controls.Add(_btnSelectFilesToMine);
            Controls.Add(_btnSaveConnectionString);
            Controls.Add(_tbConnectionString);
            Controls.Add(_rtbLogs);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "TestMinerForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Test Miner";
            ResumeLayout(false);
            PerformLayout();
        }
        private Button _btnClearLogs;
    }
}