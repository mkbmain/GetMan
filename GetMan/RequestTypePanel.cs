using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WinApp
{
    public class RequestTypePanel : Panel
    {
        public RequestType RequestType { get; private set; }
        private readonly Panel _floatingPanel = new Panel {Size = new Size(150, 55)};
        private static readonly List<string> _buttonLabel = new List<string> {"Get", "Post", "Put", "Delete"};
        private readonly RadioButton[] _radioButtons = new RadioButton[_buttonLabel.Count];

        public RequestTypePanel(Size mainFormSize)
        {
            for (int i = 0; i < _buttonLabel.Count; i++)
            {
                _radioButtons[i] = new RadioButton
                {
                    Text = _buttonLabel[i],
                    Checked = i == 0,
                    Size = new Size(55, 25),
                    Location = new Point(1, 10)
                };
                _radioButtons[i].Click += (sender, args) =>
                    RequestType = (RequestType) _buttonLabel.IndexOf(((RadioButton) sender).Text);
                _floatingPanel.Controls.Add(_radioButtons[i]);
            }
            this.Controls.Add(_floatingPanel);
            RequestType = RequestType.Get;
            InnerResize(mainFormSize);
        }

        public void InnerResize(Size mainFormSize)
        {
            for (int i = 0; i < _buttonLabel.Count; i++)
            {
                _radioButtons[i].Left = i == 0 ? 0 : _radioButtons[i - 1].Right + 10;
            }

            this.Size = new Size(mainFormSize.Width, 80);
            _floatingPanel.Width = _radioButtons.Last().Right + 10;
            _floatingPanel.Left = (mainFormSize.Width / 2) - (_floatingPanel.Width / 2);
        }
    }
}