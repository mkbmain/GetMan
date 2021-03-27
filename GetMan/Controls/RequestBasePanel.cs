using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using GetMan.Enum;

namespace GetMan.Controls
{
    public class RequestBasePanel : Panel
    {
        public RequestType RequestType { private get; set; }

        private readonly TextBox _urlTextBox = new TextBox {Location = new Point(10, 15), Width = 30};
        private readonly TabControl _tabControl = new TabControl();
        private readonly Button _runButton = new Button {Text = "Run",};
        private readonly TabPage _body = new TabPage("Body");
        private readonly RequestBodyPanel _requestBodyTabPageConent;
        private readonly TabPage _headers = new TabPage("Headers");
        private readonly HeaderView _headerTab;
        private readonly TabPage _response = new TabPage("Response");

        public RequestBasePanel(Size mainFormSize)
        {
            _tabControl.TabPages.Add(_body);
            _tabControl.TabPages.Add(_headers);
            _tabControl.TabPages.Add(_response);
            Location = new Point(1, 1);
            BorderStyle = BorderStyle.None;
            _requestBodyTabPageConent = new RequestBodyPanel(_body.Size);
            _headerTab = new HeaderView(_headers.Size);
            InnerResize(mainFormSize);
            _body.Controls.Add(_requestBodyTabPageConent);
            _headers.Controls.Add(_headerTab);
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

            var request = WebRequest.Create(_urlTextBox.Text);
            request.Method = RequestType.ToString().ToUpper();
            
            foreach (var item in  _headerTab.GetAllHeaderValues())
            {
                request.Headers.Add(item[0], item[1]);
            }
            
            switch (RequestType)
            {
                case RequestType.Post:
                case RequestType.Put:
                case RequestType.Delete:
                    // content and header todo
                    break;
                default:
                    break;
            }



            var response = (HttpWebResponse) request.GetResponse();
            string responseString = "";
            response.StatusCode.ToString();

            Stream stream = response.GetResponseStream();
            if (stream != null)
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    responseString = sr.ReadToEnd();
                }
            }


            _tabControl.SelectedTab = _response;
        }

        public void InnerResize(Size mainFormSize)
        {
            Size = new Size(mainFormSize.Width - 47, mainFormSize.Height - 136);
            _tabControl.Size = new Size(Size.Width - 35, Height - _urlTextBox.Height - 28);
            _urlTextBox.Size = new Size(_tabControl.Width - _runButton.Width, _urlTextBox.Height);
            _runButton.Location = new Point(_urlTextBox.Right + 5, _urlTextBox.Top);
            _requestBodyTabPageConent.InnerResize(_body.Size);
            _headerTab.InnerResize(_headers.Size);
        }
    }
}