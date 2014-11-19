using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Xml;

namespace Frameshop.Data
{
    internal class Serializer
    {
        public Serializer()
        {
        }

        public void Serialize(string filePath, object obj)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                XmlSerializer ser = new XmlSerializer(obj.GetType());
                ser.Serialize(fs, obj);
                fs.Flush();
                fs.Close();
            }
        }

        public T Deserialize<T>(string filePath)
        {
            T result = default(T);
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                XmlSerializer ser = new XmlSerializer(typeof(T));
                result = (T)ser.Deserialize(fs);
                fs.Close();
            }

            return result;
        }
    }
}
