using System.Drawing;
using GetMan.Enum;

namespace GetMan.Controls.SelectPanels
{
    public class ContentRadioButtonSelectPanel : BaseRadioButtonSelectPanel<ContentType>
    {
        public ContentRadioButtonSelectPanel(Size mainFormSize) : base(mainFormSize,ContentType.Raw , ContentTypeMappings.displayToEnum, "ContentTypes")
        {
        }
    }
}