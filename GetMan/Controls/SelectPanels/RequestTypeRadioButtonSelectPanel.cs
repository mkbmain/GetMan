using System.Collections.Generic;
using System.Drawing;
using GetMan.Enum;

namespace GetMan.Controls.SelectPanels
{
    public class RequestTypeRadioButtonSelectPanel : BaseRadioButtonSelectPanel<RequestType>
    {
        public RequestTypeRadioButtonSelectPanel(Size mainFormSize, RequestType enumType = RequestType.Get) : base(mainFormSize,
            enumType,
            new List<(string, string)>
            {
                ("Head", "Head"), 
                ("Get", "Get"),
                ("Post", "Post"),
                ("Patch", "Patch"), 
                ("Put", "Put"),
                ("Delete", "Delete")
            }, "Type Of Request")
        {
        }
    }
}