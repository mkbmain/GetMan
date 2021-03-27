using System;
using System.Drawing;
using System.Windows.Forms;
using GetMan.Controls.SelectPanels;

namespace GetMan.Controls.Tabs
{
    public class RequestBodyTab : BaseTabPanel
    {
        public event EventHandler ContentTypeChanged;
        private readonly TextBox _bodyTextBox = new TextBox {Location = new Point(1, 35), AutoSize = false, Multiline = true};

        private readonly ContentRadioButtonSelectPanel _contentRadioButtonSelectPanel;
        public RequestBodyTab(Size tabArea) : base(tabArea)
        {
            _contentRadioButtonSelectPanel = new ContentRadioButtonSelectPanel(tabArea)
            {
                Location = new Point(1, 1)
            };

            _contentRadioButtonSelectPanel.NewSelectionMade += (sender, args) => ContentTypeChanged?.Invoke(sender, EventArgs.Empty) ;
            
            this.Controls.Add(_contentRadioButtonSelectPanel);
            this.Controls.Add(_bodyTextBox);
            InnerResize(tabArea);
            _bodyTextBox.ScrollBars = ScrollBars.Both;
        }

        public string GetContent()
        {
            return _bodyTextBox.Text;
        }

        public  override void InnerResize(Size tabArea)
        {
            _contentRadioButtonSelectPanel.InnerResize(tabArea);
            _bodyTextBox.Location = new Point(1, _contentRadioButtonSelectPanel.Bottom + 15);
            _bodyTextBox.Size = new Size(tabArea.Width-10, tabArea.Height-_contentRadioButtonSelectPanel.Height-15);
            base.InnerResize(tabArea);
        }
    }
}