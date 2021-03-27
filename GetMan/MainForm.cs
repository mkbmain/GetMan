using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GetMan.Controls;
using GetMan.Enum;

namespace GetMan
{
    internal class MainForm : Form
    {
        private readonly RequestTypePanel _requestTypePanel;
        private readonly LeftPanel _leftPanel;
        private readonly RequestBasePanel _requestBasePanel;

        public MainForm()
        {
            this.Text = "GET MAN";
            this.Size = new Size(300, 200);
            _leftPanel = new LeftPanel(this.Size);
            _requestTypePanel = new RequestTypePanel(this.Size);
            _requestBasePanel = new RequestBasePanel(Size) {Location = new Point(36, 100)};
            _requestTypePanel.NewSelectionMade +=
                (sender, args) => _requestBasePanel.RequestType = (RequestType) sender;
            this.Resize += OnResize;
            this.Controls.Add(_leftPanel);
            this.Controls.Add(_requestTypePanel);
            this.Controls.Add(_requestBasePanel);
        }

        private void OnResize(object sender, EventArgs e)
        {
            _requestTypePanel.InnerResize(this.Size);
            _leftPanel.InnerResize(this.Size);
            _requestBasePanel.InnerResize(this.Size);
        }


        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }
    }
}