using System.Collections.Generic;

namespace GetMan
{
    public static class ContentTypeMappings
    {
        public static readonly List<(string Display, string EnumMap)> DisplayToEnum =
            new List<(string Display, string EnumMap)>()
                {("Json", "Json"), ("Xml", "Xml"),("Raw","Raw"),("None","None")};
        
        public static readonly List<(string Action, string EnumMap)> EnumToAction =
            new List<(string Action, string EnumMap)>()
                {("application/json", "Json"), ( "text/xml","Xml"),("","Raw"),(null,"None")};


    }
}