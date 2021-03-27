using System.Drawing;
using System.Windows.Forms;

namespace GetMan.Controls.Tabs
{
    public class ResponseBodyTab : BaseTabPanel
    {

        private readonly TextBox _bodyTextBox = new TextBox {Location = new Point(1, 35), AutoSize = false, Multiline = true};
        private readonly Label _result = new Label {Location = new Point(1, 1), AutoSize = true};
        public ResponseBodyTab(Size tabArea) : base(tabArea)
        {
            this.Controls.Add(_bodyTextBox);
            this.Controls.Add(_result);
            InnerResize(tabArea);
            _bodyTextBox.ScrollBars = ScrollBars.Both;
        }

        public void Set(string body,string status)
        {
            _bodyTextBox.Text = body;
            _result.Text = status;
        }
        
        

        public  override void InnerResize(Size tabArea)
        {
            _bodyTextBox.Location = new Point(1, _result.Bottom + 15);
            _bodyTextBox.Size = new Size(tabArea.Width-10, tabArea.Height-_result.Height-15);
            base.InnerResize(tabArea);
        }
    }
}