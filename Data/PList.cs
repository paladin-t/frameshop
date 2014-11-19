using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Diagnostics;
using System.IO;

namespace Frameshop.Data
{
    internal class PList : Dictionary<string, dynamic>
    {
        public new dynamic this[string key]
        {
            get
            {
                if (!ContainsKey(key))
                {
                    OnKeyNotFound(key);

                    return null;
                }

                return base[key];
            }
            set
            {
                base[key] = value;
            }
        }

        public static event PListKeyNotFoundExceptionEventHandler KeyNotFound;

        public PList()
        {
        }

        public PList(string file)
        {
            Load(file);
        }

        public void Load(string file)
        {
            string text = File.ReadAllText(file);
            Parse(text);
        }

        public void Save(string file)
        {
            string text = Format();
            File.WriteAllText(file, text);
        }

        public void Parse(string text)
        {
            Clear();

            XDocument doc = XDocument.Parse(text);
            XElement plist = doc.Element("plist");
            XElement dict = plist.Element("dict");

            var dictElements = dict.Elements();
            ParseDict(this, dictElements);
        }

        public string Format()
        {
            XDocument doc = new XDocument
            (
                new XElement
                (
                    "plist",
                    new XElement("dict")
                )
            );

            XElement dict = doc.Element("plist").Element("dict");
            FormatDict(this, dict);

            return doc.ToString();
        }

        private void ParseDict(PList dict, IEnumerable<XElement> elements)
        {
            for (int i = 0; i < elements.Count(); i += 2)
            {
                XElement key = elements.ElementAt(i);
                XElement val = elements.ElementAt(i + 1);

                dict[key.Value] = ParseValue(val);
            }
        }

        private List<dynamic> ParseArray(IEnumerable<XElement> elements)
        {
            List<dynamic> list = new List<dynamic>();
            foreach (XElement e in elements)
            {
                dynamic one = ParseValue(e);
                list.Add(one);
            }

            return list;
        }

        private dynamic ParseValue(XElement val)
        {
            switch (val.Name.ToString())
            {
                case "string":
                    return val.Value;
                case "integer":
                    return int.Parse(val.Value);
                case "real":
                    return float.Parse(val.Value);
                case "true":
                    return true;
                case "false":
                    return false;
                case "dict":
                    PList plist = new PList();
                    ParseDict(plist, val.Elements());
                    return plist;
                case "array":
                    List<dynamic> list = ParseArray(val.Elements());
                    return list;
                default:
                    throw new ArgumentException("Unsupported");
            }
        }

        private void FormatDict(PList dict, XElement element)
        {
            foreach (var kv in dict)
            {
                string key = kv.Key;
                dynamic val = kv.Value;

                XElement keyElem = new XElement("key");
                keyElem.Value = key;
                XElement valElem = FormatValue(val);
                element.Add(keyElem);
                element.Add(valElem);
            }
        }

        private void FormatArray(List<dynamic> val, XElement element)
        {
            foreach (dynamic child in val)
            {
                XElement elem = FormatValue(child);
                element.Add(elem);
            }
        }

        private XElement FormatValue(dynamic val)
        {
            XElement elem = null;
            Type t = val.GetType();
            if (t == typeof(string))
            {
                elem = new XElement("string");
                elem.Value = val;
            }
            else if (t == typeof(int))
            {
                elem = new XElement("integer");
                elem.Value = val.ToString();
            }
            else if (t == typeof(float))
            {
                elem = new XElement("real");
                elem.Value = val.ToString();
            }
            else if (t == typeof(bool))
            {
                if (val)
                    elem = new XElement("true");
                else
                    elem = new XElement("false");
            }
            else if (t == typeof(PList))
            {
                elem = new XElement("dict");
                FormatDict(val, elem);
            }
            else if (t == typeof(List<dynamic>))
            {
                elem = new XElement("array");
                FormatArray(val, elem);
            }
            else
            {
                Debug.Fail("Unknow type");
            }

            return elem;
        }

        private void OnKeyNotFound(string key)
        {
            if (KeyNotFound != null)
                KeyNotFound(this, new PListKeyNotFoundExceptionEventArgs(key));
        }
    }

    internal class PListKeyNotFoundExceptionEventArgs : EventArgs
    {
        public string Key { get; set; }

        public PListKeyNotFoundExceptionEventArgs(string key)
        {
            Key = key;
        }
    }

    internal delegate void PListKeyNotFoundExceptionEventHandler(PList sender, PListKeyNotFoundExceptionEventArgs e);

    internal class PListKeyNotFoundExceptionEventHolder : IDisposable
    {
        private PListKeyNotFoundExceptionEventHandler handler = null;

        public PListKeyNotFoundExceptionEventHolder(PListKeyNotFoundExceptionEventHandler hdl)
        {
            PList.KeyNotFound += hdl;
            handler = hdl;
        }

        public void Dispose()
        {
            PList.KeyNotFound -= handler;
        }
    }
}
