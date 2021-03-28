using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GetMan.Controls.Tabs
{
    public class HeaderView : Panel
    {
        private readonly List<(TextBox header, TextBox value)> _headerPairs = new List<(TextBox, TextBox)>();
        private Panel _displayPanel = new Panel();
        private Button _addRow = new Button {Text = "+"};
        private Size _containerSizeLast;

        public HeaderView(Size containerSize)
        {
            AutoScroll = true;
            _addRow.Click += (sender, args) => UpdateOrInsert("", "", _containerSizeLast);
            this.Controls.Add(_displayPanel);
            UpdateOrInsert("", "", containerSize);
        }

        public void UpdateOrInsert(string headerName, string value, Size tabSize,bool editable=true)
        {
            var header = _headerPairs.FirstOrDefault(f =>
                f.header.Text == headerName && string.IsNullOrWhiteSpace(f.header.Text) == false);
            if (header.header == null)
            {
                _headerPairs.Add((new TextBox {Text = headerName,ReadOnly = !editable}, new TextBox {Text = value,ReadOnly = !editable}));
            }
            else
            {
                header.value.Text = value;
                header.header.ReadOnly = !editable;
                header.value.ReadOnly = !editable;
            }

            InnerResize(tabSize);
        }
        
        public void Remove(string headerName,Size tabSize)
        {
            var header = _headerPairs.FirstOrDefault(f =>
                f.header.Text == headerName);
            _headerPairs.Remove(header);
            InnerResize(tabSize);
        }

        public IEnumerable<string[]> GetAllHeaderValues()
        {
            return  _headerPairs
                .Where(f=> !string.IsNullOrWhiteSpace(f.header.Text) && !string.IsNullOrWhiteSpace(f.value.Text))
                .Select(f => new[] {f.header.Text, f.value.Text})
                .ToArray();
        }

        private void SetupTextBox(TextBox header, TextBox value, int top, int width)
        {
            header.Location = new Point(1, top);
            header.Height = 25;
            value.Height = 25;
            header.Width = width;
            value.Width = width;
            value.Location = new Point(header.Right, top);
        }

        public void InnerResize(Size tabSize)
        {
            this.Size = new Size(tabSize.Width - 15, tabSize.Height - 55);
            this.Controls.Remove(_displayPanel);
            _displayPanel = new Panel {Height = Size.Height, Width = Size.Width};

            int top = 25;
            int width = (int) (Size.Width * 0.5) - (int) (_addRow.Width * 1.5) - 15;
            var headerTitle = new TextBox
            {
                Text = "Header",
                TextAlign = HorizontalAlignment.Center,
                ReadOnly = true
            };
            var valueTitle = new TextBox
            {
                Text = "Value",
                TextAlign = HorizontalAlignment.Center,
                ReadOnly = true
            };
            SetupTextBox(headerTitle, valueTitle, 0, width);
            _displayPanel.Controls.Add(headerTitle);
            _displayPanel.Controls.Add(valueTitle);
            foreach (var pair in _headerPairs)
            {
                SetupTextBox(pair.header, pair.value, top, width);
                top += pair.header.Height;
                _displayPanel.Controls.Add(pair.header);
                _displayPanel.Controls.Add(pair.value);
            }

            _displayPanel.Controls.Add(_addRow);
            this.Controls.Add(_displayPanel);
            _addRow.Top = top - 18;
            _addRow.BringToFront();
            _addRow.Left = _headerPairs.Last().value.Right + 5;
            _containerSizeLast = tabSize;
        }
    }
}