using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Data;
using System.Xml;

namespace ResourcesLib.UI
{
    public class UIDataset
    {
        internal static DataSet ReadDataSet()
        {
            string xml_xsd = Properties.Resources.UI_XSD;
            string xml = Properties.Resources.UI_XML;
            DataSet ds = new DataSet();

            byte[] array_xsd = Encoding.UTF8.GetBytes(xml_xsd);
            byte[] array_xml = Encoding.UTF8.GetBytes(xml);
            
            MemoryStream stream_xsd = new MemoryStream(array_xsd);
            MemoryStream stream_xml = new MemoryStream(array_xml);
            ds.ReadXmlSchema(stream_xsd);           
            ds.ReadXml(stream_xml);
            return ds;
        }
    }
}
