using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GetMan.Controls
{
    public abstract class FloatingRadioButtonBox<T> : Panel where T : System.Enum
    {
        public event EventHandler NewSelectionMade;
        private T _enumType;
        private readonly GroupBox _floatingPanel = new GroupBox {Size = new Size(150, 55),};
        private readonly List<(string, string)> _buttonLabel;
        private readonly RadioButton[] _radioButtons;
        
        private T EnumType
        {
            get => _enumType;
            set
            {
                _enumType = value;
                NewSelectionMade?.Invoke(_enumType, EventArgs.Empty);
            }
        }

        public FloatingRadioButtonBox(Size mainFormSize, T enumType,
            IEnumerable<(string title, string enumMapValue)> optionLabels, string optionSelectionTitle)
        {
            _buttonLabel = optionLabels.ToList();
            _floatingPanel.Text = optionSelectionTitle;
            _radioButtons = new RadioButton[_buttonLabel.Count];
            for (int i = 0; i < _buttonLabel.Count; i++)
            {
                _radioButtons[i] = new RadioButton
                {
                    Text = _buttonLabel[i].Item1,
                    Checked = i == 0,
                    Size = new Size(55, 25),
                    Location = new Point(5, 15)
                };

                _radioButtons[i].Click += (sender, args) =>
                    EnumType = (T) System.Enum.Parse(typeof(T),
                        _buttonLabel.First(f => f.Item1 == ((RadioButton) sender).Text).Item2);
                _floatingPanel.Controls.Add(_radioButtons[i]);
            }

            Controls.Add(_floatingPanel);
            InnerResize(mainFormSize);
            EnumType = enumType;
            _radioButtons.First(f => f.Text == EnumType.ToString()).Checked = true;
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