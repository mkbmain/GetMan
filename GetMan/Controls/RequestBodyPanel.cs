using System;
using System.Drawing;
using System.Windows.Forms;

namespace GetMan.Controls
{
    public class RequestBodyPanel : BaseTabPanel
    {
        public event EventHandler ContentTypeChanged;
        private readonly TextBox _bodyTextBox = new TextBox {Location = new Point(1, 35), AutoSize = false, Multiline = true};

        private readonly ContentSelectPanel _contentSelectPanel;
        public RequestBodyPanel(Size tabArea) : base(tabArea)
        {
            _contentSelectPanel = new ContentSelectPanel(tabArea)
            {
                Location = new Point(1, 1)
            };

            _contentSelectPanel.NewSelectionMade += (sender, args) => ContentTypeChanged?.Invoke(sender, EventArgs.Empty) ;
            
            this.Controls.Add(_contentSelectPanel);
            this.Controls.Add(_bodyTextBox);
            InnerResize(tabArea);
        }

        public  override void InnerResize(Size tabArea)
        {
            _contentSelectPanel.InnerResize(tabArea);
            _bodyTextBox.Location = new Point(1, _contentSelectPanel.Bottom + 15);
            _bodyTextBox.Size = new Size(tabArea.Width-10, tabArea.Height-_contentSelectPanel.Height-15);
            base.InnerResize(tabArea);
        }
    }
}