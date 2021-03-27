using System;
using System.Drawing;
using System.Windows.Forms;

namespace GetMan.Controls
{
    public class LeftPanel : Panel
    {
        private readonly Label _extendedLabel;

        public LeftPanel(Size mainFormSize)
        {
            _extendedLabel = new Label {Text = ">>", Size = new Size(30, 30), Location = new Point(1, 1)};
            this.Controls.Add(_extendedLabel);
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Size = new Size(30, mainFormSize.Height);
            _extendedLabel.Click += ExtendedLabelOnClick;
        }

        private void ExtendedLabelOnClick(object sender, EventArgs e)
        {
            if (_extendedLabel.Text == ">>")
            {
                this.Width = 105;
                _extendedLabel.Location = new Point(this.Width - _extendedLabel.Width, 1);
                _extendedLabel.Text = "<<";
                return;
            }

            this.Width = 30;
            _extendedLabel.Location = new Point(1, 1);
            _extendedLabel.Text = ">>";
        }


        public void InnerResize(Size mainFormSize)
        {
            this.Height = mainFormSize.Height;
        }
    }
}