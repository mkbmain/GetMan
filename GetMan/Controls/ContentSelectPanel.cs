using System.Drawing;

namespace GetMan.Controls
{
    public class ContentSelectPanel : FloatingRadioButtonBox<ContentType>
    {
        public ContentSelectPanel(Size mainFormSize) : base(mainFormSize,ContentType.Raw , ContentTypeMappings.displayToEnum, "ContentTypes")
        {
        }
    }
}