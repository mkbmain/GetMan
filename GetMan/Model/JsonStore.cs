using System.Collections.Generic;
using GetMan.Enum;

namespace GetMan.Model
{
    public class JsonStore
    {
        public RequestType RequestType { get; set; }
        public string Url { get; set; }
        public List<(string name,string value)> Headers { get; set; } 
        public string Body { get; set; }
    }
}