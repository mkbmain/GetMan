using System.Collections.Generic;

namespace GetMan
{
    public class ContentTypeMappings
    {
        public static List<(string Display, string EnumMap)> displayToEnum =
            new List<(string Display, string EnumMap)>()
                {("Json", "Json"), ("Xml", "Xml"),("Raw","Raw")};
        
        public static List<(string Action, string EnumMap)> EnumToAction =
            new List<(string Action, string EnumMap)>()
                {("application/json", "Json"), ( "text/xml","Xml"),("","Raw")};


    }
}