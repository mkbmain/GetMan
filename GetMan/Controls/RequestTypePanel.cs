using System.Collections.Generic;
using System.Drawing;
using GetMan.Enum;

namespace GetMan.Controls
{
    public class RequestTypePanel : FloatingRadioButtonBox<RequestType>
    {
        public RequestTypePanel(Size mainFormSize, RequestType enumType = RequestType.Get) : base(mainFormSize,
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