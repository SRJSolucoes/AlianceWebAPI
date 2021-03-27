using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadraoWebApi.Services
{
    public class XMLService
    {
        public static void ReadXML(DataSet ds, String xml)
        {
            ds.EnforceConstraints = false;
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                ds.ReadXml(stream);
            }
        }
    }
}
