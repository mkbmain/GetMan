using System.Drawing;
using System.Windows.Forms;

namespace GetMan.Controls.Tabs
{
    public abstract class BaseTabPanel : Panel
    {
        public BaseTabPanel(Size tabArea)
        {
            
        }

        public virtual void InnerResize(Size tabArea)
        {
            this.Size = tabArea;
        }
    }
}