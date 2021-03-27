using System;
using System.Windows.Forms;
namespace WinApp
{
    internal class MainForm : Form
    {
        public MainForm()
        {
            RequestTypePanel requestTypePanel = new RequestTypePanel(this.Size);
            this.Resize += (sender, args) => requestTypePanel.InnerResize(this.Size);
            
            this.Controls.Add(requestTypePanel);
        }
        
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }
    }
}