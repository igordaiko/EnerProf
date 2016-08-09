using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Forms;
using EnerProf.Models;
using System.Web;
using System.Web.Mvc;

namespace EnerProf.DataBaseClasses
{
    class XMLReader
    {
        int id;
        XDocument document;
        public IEnumerable<XElement> node;
        public IEnumerable<XElement> type;
        string link = HttpContext.Current.Server.MapPath(@"\App_Data\Products_types.xml");
        public XMLReader(int id)
        {
            this.id = id;
            document = XDocument.Load(link);
            node = from xe in document.Descendants("product")
                   where xe.Attribute("id").Value == id.ToString()
                   select xe;
        }
        public XMLReader(int id, string type_name)
        {
            this.id = id;
            document = XDocument.Load(link);
            node = from xe in document.Descendants("product")
                   where xe.Attribute("id").Value == id.ToString()
                   select xe;
            foreach (var n in node)
                type = from xe in n.Descendants("type")
                       where xe.Attribute("name").Value == type_name
                       select xe;
        }
        public List<Product_Type> SelectTypes(IEnumerable<XElement> node)
        {
            IEnumerable<XElement> types = null;
            List<Product_Type> types_names = new List<Product_Type>();
            foreach (var n in node)
                types = from xe in n.Descendants("type")
                        select xe;
            Product_Type type;
            List<XElement> list;
            foreach (var t in types)
            {
                list = new List<XElement>();
                list.Add(t);
                type = new Product_Type();
                type.Name = t.Attribute("name").Value;
                type.ID = t.Attribute("id").Value;
                type.Properies = SelectProps((IEnumerable<XElement>)list);
                types_names.Add(type);
            }
            return types_names;
        }

        public List<Property> SelectProps(IEnumerable<XElement> node)
        {
            IEnumerable<XElement> props = null;
            List<Property> properties = new List<Property>();
            foreach (var n in node)
                props = from xe in n.Elements("property")
                        select xe;
            Property prop;
            if (props != null)
            {
                foreach (var p in props)
                {
                    prop = new Property();
                    prop.Name = p.Attribute("name").Value;
                    if (p.Attribute("value") != null)
                        prop.Value = p.Attribute("value").Value;
                    prop.ID = p.Attribute("id").Value;
                    properties.Add(prop);
                }
            }
            return properties;
        }

        public string SelectValueOfAttribute(IEnumerable<XElement> node, string name)
        {
            string value = null;
            foreach (var n in node)
                value = n.Attribute("Name").Value;
            return value;
        }
        public void AddNewNode(string tag_name, string parent_tag, string parent_id, Dictionary<string, string> attributes)
        {
            XElement parent = null;
            XAttribute[] attrs = new XAttribute[attributes.Count];
            var node = from xe in document.Descendants(parent_tag)
                       where xe.Attribute("id").Value == parent_id
                       select xe;
            foreach (var n in node)
                parent = n;
            for (int i = 0; i < attrs.Length; i++)
            {
                attrs[i] = new XAttribute(attributes.Keys.ToArray<string>()[i], attributes.Values.ToArray<string>()[i]);
            }
            XElement element = new XElement(tag_name, attrs);
            parent.Add(element);
            if (parent_tag == "product" && tag_name == "property")
            {
                node = from xe in parent.Descendants("type")
                       select xe;
                foreach (var n in node)
                {
                    parent = n;
                    parent.Add(element);
                }
            }
            document.Save(link);
        }
        public void ChangeNode(string tag_name, string parent_name, string parent_id, string id, Dictionary<string, string> attributes)
        {
            var parent_e = from xe in document.Descendants(parent_name)
                           where xe.Attribute("id").Value == parent_id
                           select xe;
            XElement parent_node = null;
            foreach (var parent in parent_e)
            {
                parent_node = parent;
            }
            var e = from xe in parent_node.Descendants(tag_name)
                    where xe.Attribute("id").Value == id
                    select xe;
            XElement element = null;
            Dictionary<string, string> buff = new Dictionary<string, string>();
            foreach (var buf_attr in attributes)
                buff.Add(buf_attr.Key, buf_attr.Value);
            foreach (var el in e)
            {
                element = el;
                attributes = buff;
                foreach (var attr in element.Attributes())
                {
                    bool b = false;
                    foreach (string attribute in attributes.Keys)
                        if (attribute == attr.Name)
                            element.Attribute(attribute).Value = attributes[attribute];
                    if (b)
                        attributes.Remove(attr.Name.ToString());
                }
                if (attributes.Count != 0)
                    foreach (string attribute in attributes.Keys)
                        element.SetAttributeValue(attribute, attributes[attribute]);
            }
            document.Save(link);

        }
        public void DeleteNode(string id)
        {
            XmlDocument document = new XmlDocument();
            document.Load(link);
            string query = String.Format("//*[@id='{0}']", id);
            XmlNodeList node_list = document.SelectNodes(query);
            foreach (XmlNode node in node_list)
                node.ParentNode.RemoveChild(document.SelectSingleNode(query));

            document.Save(link);
        }
    }
}
