using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GetMan.Controls;
using GetMan.Controls.SelectPanels;
using GetMan.Enum;

namespace GetMan
{
    internal class MainForm : Form
    {
        private readonly RequestTypeRadioButtonSelectPanel _requestTypeRadioButtonSelectPanel;
        private readonly LeftPanel _leftPanel;
        private readonly ClientPanel _clientPanel;

        public MainForm()
        {
            this.Text = "GET MAN";
            this.Size = new Size(300, 200);
            _leftPanel = new LeftPanel(this.Size);
            _requestTypeRadioButtonSelectPanel = new RequestTypeRadioButtonSelectPanel(this.Size);
            _clientPanel = new ClientPanel(Size) {Location = new Point(36, 100)};
            _requestTypeRadioButtonSelectPanel.NewSelectionMade +=
                (sender, args) => _clientPanel.RequestType = (RequestType) sender;
            this.Resize += OnResize;
            this.Controls.Add(_leftPanel);
            this.Controls.Add(_requestTypeRadioButtonSelectPanel);
            this.Controls.Add(_clientPanel);
        }

        private void OnResize(object sender, EventArgs e)
        {
            _requestTypeRadioButtonSelectPanel.InnerResize(this.Size);
            _leftPanel.InnerResize(this.Size);
            _clientPanel.InnerResize(this.Size);
        }


        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }
    }
}