using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Com.Gitusme.Net.Extensiones.Demos.Model
{
    public class MyObject
    {
        public String Name { get; set; } = "gitusme";
    }


    [XmlRoot("root")]
    public class MyXmlObject
    {
        [XmlAttribute("id")]
        public int Id { get; set; } = default(int);

        [XmlElement("name")]
        public string Name { get; set; } = "Xml Object";
    }

    public class MyJsonObject
    {
        public int Id { get; set; } = default(int);

        public string Name { get; set; } = "Json Object";
    }
}
