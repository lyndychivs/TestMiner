namespace TestMiner.WindowsApplication
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal partial class TestMiner : Form
    {
        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            var resources = new ComponentResourceManager(typeof(TestMiner));
            richTextBox1 = new RichTextBox();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(12, 132);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(956, 563);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            // 
            // TestMiner
            // 
            ClientSize = new Size(980, 707);
            Controls.Add(richTextBox1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "TestMiner";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Test Miner";
            ResumeLayout(false);
        }
        private RichTextBox richTextBox1;
    }
}