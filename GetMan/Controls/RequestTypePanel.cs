using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GetMan.Enum;

namespace GetMan.Controls
{
    public class RequestTypePanel : Panel
    {
        public event EventHandler RequestTypeChange;
        private RequestType _requestType;

        private RequestType RequestType
        {
            get => _requestType;
            set
            {
                _requestType = value;
                RequestTypeChange?.Invoke(_requestType, EventArgs.Empty);
            }
        }

        private readonly GroupBox _floatingPanel = new GroupBox {Size = new Size(150, 55), Text = "Type Of Request"};
        private static readonly List<string> _buttonLabel = new List<string> {"Get", "Post", "Put", "Delete"};
        private readonly RadioButton[] _radioButtons = new RadioButton[_buttonLabel.Count];

        public RequestTypePanel(Size mainFormSize, RequestType requestType = RequestType.Get)
        {
            for (int i = 0; i < _buttonLabel.Count; i++)
            {
                _radioButtons[i] = new RadioButton
                {
                    Text = _buttonLabel[i],
                    Checked = i == 0,
                    Size = new Size(55, 25),
                    Location = new Point(5, 15)
                };
                _radioButtons[i].Click += (sender, args) =>
                    RequestType = (RequestType) _buttonLabel.IndexOf(((RadioButton) sender).Text);
                _floatingPanel.Controls.Add(_radioButtons[i]);
            }

            Controls.Add(_floatingPanel);
            InnerResize(mainFormSize);
            RequestType = requestType;
            _radioButtons[(int) RequestType].Checked = true;
        }

        public void InnerResize(Size mainFormSize)
        {
            for (int i = 0; i < _buttonLabel.Count; i++)
            {
                _radioButtons[i].Left = i == 0 ? 10 : _radioButtons[i - 1].Right + 10;
            }

            this.Size = new Size(mainFormSize.Width, 80);
            _floatingPanel.Width = _radioButtons.Last().Right + 10;
            _floatingPanel.Left = (mainFormSize.Width / 2) - (_floatingPanel.Width / 2);
        }
    }
}