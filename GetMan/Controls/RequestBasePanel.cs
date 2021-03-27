using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using GetMan.Enum;

namespace GetMan.Controls
{
    public class RequestBasePanel : Panel
    {
        public RequestType RequestType { get; set; }

        private readonly TextBox _urlTextBox = new TextBox {Location = new Point(10, 15), Width = 30};
        private readonly TabControl _tabControl = new TabControl();
        private readonly Button _runButton = new Button {Text = "Run",};
        private readonly TabPage _body = new TabPage("Body");
        private readonly TabPage _headers = new TabPage("Headers");

        public RequestBasePanel(Size mainFormSize)
        {
            _tabControl.TabPages.Add(_body);
            _tabControl.TabPages.Add(_headers);
            Location = new Point(1, 1);
            BorderStyle = BorderStyle.None;
            InnerResize(mainFormSize);
            Controls.Add(_urlTextBox);
            Controls.Add(_runButton);
            Controls.Add(_tabControl);
            _tabControl.Location = new Point(10, _urlTextBox.Bottom + 10);
            _runButton.Click += RunButtonOnClick;
        }

        public string JsonMe()
        {
            return "";
        }

        private void RunButtonOnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_urlTextBox.Text))
            {
                MessageBox.Show("Check Url");
                return;
            }

            if (!_urlTextBox.Text.ToLower().StartsWith("http"))
            {
                _urlTextBox.Text = $"http://{_urlTextBox.Text}";
            }

            WebClient webClient = new WebClient();
            var request = WebRequest.Create(_urlTextBox.Text);
            request.Method = RequestType.ToString().ToUpper();
        }

        public void InnerResize(Size mainFormSize)
        {
            Size = new Size(mainFormSize.Width - 47, mainFormSize.Height - 136);
            _tabControl.Size = new Size(Size.Width - 35, Height - _urlTextBox.Height - 28);
            _urlTextBox.Size = new Size(_tabControl.Width - _runButton.Width, _urlTextBox.Height);

            _runButton.Location = new Point(_urlTextBox.Right + 5, _urlTextBox.Top);
        }
    }
}