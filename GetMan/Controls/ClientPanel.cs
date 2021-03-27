using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using GetMan.Controls.Tabs;
using GetMan.Enum;

namespace GetMan.Controls
{
    public class ClientPanel : Panel
    {
        public RequestType RequestType { private get; set; }

        private ContentType _contentType;


        private readonly TextBox _urlTextBox = new TextBox {Location = new Point(10, 15), Width = 30};
        private readonly TabControl _tabControl = new TabControl();
        private readonly Button _runButton = new Button {Text = "Run",};
        private readonly TabPage _body = new TabPage("Body");
        private readonly RequestBodyTab _requestBodyTabPage;
        private readonly TabPage _headers = new TabPage("Headers");
        private readonly HeaderView _headerTab;
        private readonly ResponseBodyTab _responseTab;
        private readonly TabPage _response = new TabPage("Response");

        public ClientPanel(Size mainFormSize)
        {
            _tabControl.TabPages.Add(_body);
            _tabControl.TabPages.Add(_headers);
            _tabControl.TabPages.Add(_response);
            _responseTab = new ResponseBodyTab(_response.Size);
            _headerTab = new HeaderView(_headers.Size);
            Location = new Point(1, 1);
            BorderStyle = BorderStyle.None;
            _requestBodyTabPage = new RequestBodyTab(_body.Size);
            _requestBodyTabPage.ContentTypeChanged += (sender, args) => ContentType = (ContentType) sender;
            InnerResize(mainFormSize);
            _body.Controls.Add(_requestBodyTabPage);
            _headers.Controls.Add(_headerTab);
            _response.Controls.Add(_responseTab);
            Controls.Add(_urlTextBox);
            Controls.Add(_runButton);
            Controls.Add(_tabControl);
            _tabControl.Location = new Point(10, _urlTextBox.Bottom + 10);
            _runButton.Click += RunButtonOnClick;
        }

        public ContentType ContentType
        {
            get => _contentType;
            set
            {
                _contentType = value;
                switch (ContentType)
                {
                    case ContentType.Json:
                    case ContentType.Xml:
                        _headerTab.UpdateOrInsert("ContentType",
                            ContentTypeMappings.EnumToAction.First(f => f.EnumMap == value.ToString()).Action,
                            _headers.Size, false);
                        break;
                    case ContentType.Raw:
                        _headerTab.UpdateOrInsert("ContentType", "", _headers.Size);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
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

            foreach (var item in _headerTab.GetAllHeaderValues())
            {
                request.Headers.Add(item[0], item[1]);
            }

            switch (RequestType)
            {
                case RequestType.Post:
                case RequestType.Put:
                case RequestType.Patch:
                case RequestType.Delete:
                    var body = _requestBodyTabPage.GetContent();
                    if (!string.IsNullOrWhiteSpace(body))
                    {
                        using (var sw = new StreamWriter(request.GetRequestStream()))
                        {
                            sw.Write(body);
                        }
                    }

                    break;
            }


            var response = (HttpWebResponse) request.GetResponse();
            string responseString = "";

            Stream stream = response.GetResponseStream();
            if (stream != null)
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    responseString = sr.ReadToEnd();
                }
            }

            _responseTab.Set(responseString, $"status : {(int) response.StatusCode}({response.StatusCode.ToString()})");

            _tabControl.SelectedTab = _response;
        }

        public void InnerResize(Size mainFormSize)
        {
            Size = new Size(mainFormSize.Width - 47, mainFormSize.Height - 136);
            _tabControl.Size = new Size(Size.Width - 35, Height - _urlTextBox.Height - 28);
            _urlTextBox.Size = new Size(_tabControl.Width - _runButton.Width, _urlTextBox.Height);
            _runButton.Location = new Point(_urlTextBox.Right + 5, _urlTextBox.Top);
            _requestBodyTabPage.InnerResize(_body.Size);
            _headerTab.InnerResize(_headers.Size);
            _responseTab.InnerResize(_response.Size);
        }
    }
}