namespace TestMiner.WindowsApplication
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;

    using Microsoft.Extensions.Logging;

    using Serilog;
    using Serilog.Events;
    using Serilog.Extensions.Logging;

    using TestMiner.Logger;
    using TestMiner.Utility;

    internal partial class TestMinerForm : Form
    {
        private readonly ILogWrapper _logWrapper;

        private readonly ConnectionManager _connectionManager;

        private readonly ConnectionStringValidator _connectionStringValidator;

        private TestMinerApplication _testMinerApplication = default!;

        public TestMinerForm()
        {
            InitializeComponent();

            _logWrapper = new LogWrapper(
                new SerilogLoggerFactory(
                    new LoggerConfiguration()
                    .MinimumLevel.Verbose()
                    .WriteTo.RichTextBox(
                        _rtbLogs,
                        LogEventLevel.Verbose)
                    .WriteTo.File(
                        $"Logs\\{nameof(TestMiner)}.log",
                        restrictedToMinimumLevel: LogEventLevel.Verbose,
                        rollingInterval: RollingInterval.Day,
                        retainedFileCountLimit: 3)
                    .CreateLogger()).CreateLogger<ILogWrapper>());

            _connectionManager = new ConnectionManager(_logWrapper);

            _connectionStringValidator = new ConnectionStringValidator(_logWrapper);
        }

        private void BtnSaveConnectionString_Click(object sender, EventArgs eventArgs)
        {
            if (!_connectionStringValidator.IsConnectionStringValid(_tbConnectionString.Text))
            {
                return;
            }

            _connectionManager.SaveConnectionString(_tbConnectionString.Text);
        }

        private void BtnSelectFilesToMine_Click(object sender, EventArgs eventArgs)
        {
            if (_openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            foreach (string fileName in _openFileDialog.FileNames)
            {
                _listBoxFilesSelected.Items.Add(fileName);
            }
        }

        private void BtnClearListBoxFilesSelected_Click(object sender, EventArgs eventArgs)
        {
            _listBoxFilesSelected.Items.Clear();
        }

        private void BtnMine_Click(object sender, EventArgs e)
        {
            string connectionString = _connectionManager.GetConnectionString(_tbConnectionString.Text);

            if (!_connectionStringValidator.IsConnectionStringValid(connectionString))
            {
                return;
            }

            _testMinerApplication = new TestMinerApplication(_logWrapper, connectionString);

            var filePaths = new List<string>();
            foreach (string fileName in _listBoxFilesSelected.Items)
            {
                filePaths.Add(fileName);
            }

            Cursor = Cursors.WaitCursor;
            _testMinerApplication.MineFiles(filePaths);
            Cursor = Cursors.Default;
        }

        private void BtnVerifyConnectionString_Click(object sender, EventArgs e)
        {
            string connectionString = _connectionManager.GetConnectionString(_tbConnectionString.Text);

            if (_connectionStringValidator.IsConnectionStringValid(connectionString))
            {
                _lblConnectionStringStatus.Text = "Connection String: Valid";
                _lblConnectionStringStatus.ForeColor = Color.Green;
            }
            else
            {
                _lblConnectionStringStatus.Text = "Connection String: Invalid";
                _lblConnectionStringStatus.ForeColor = Color.Red;
            }
        }

        private void LblGithub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://github.com/lyndychivs/TestMiner",
                    UseShellExecute = true,
                });
            }
            catch
            {
            }
        }

        private void BtnClearLogs_Click(object sender, EventArgs e)
        {
            _rtbLogs.Clear();
        }
    }
}