using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Xml;

namespace CPU
{
	public class XmlNode
	{
		// =========== Public Properties ======================================

		public XmlNode Parent { get; private set; }

		public virtual string XmlTag { get { throw new Exception("No XML Tag! -> " + this.GetType()); } }

		// =========== Constructor ============================================

		public XmlNode(XmlNode parent) 
		{
			this.Parent = parent;
		}

		// =========== Public XML Methods =====================================

		public virtual void ReadXml(string xml) 
		{
			using (StringReader sr = new StringReader(xml))
			{
				using (XmlReader reader = XmlReader.Create(sr))
				{
					reader.Read();
					while (reader.NodeType == XmlNodeType.Whitespace)
						reader.Read();

					if (reader.NodeType != XmlNodeType.Element || reader.LocalName != this.XmlTag)
						throw new Exception("Bad formed XML string, must be node [" + this.XmlTag + "]");

					this.ReadXml(reader);
				}
			}
		}

		public virtual void ReadXml(XmlReader reader) 
		{
			this.XmlReadAttributes(reader);

			if (reader.IsEmptyElement)
				return;

			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.Element)
					this.XmlReadNodes(reader);

				else if (reader.NodeType == XmlNodeType.EndElement && reader.LocalName == this.XmlTag)
					break;
			}
		}

		public virtual void WriteXml(XmlTextWriter writer) 
		{
			writer.WriteStartElement(this.XmlTag);
			this.XmlWriteAttributes(writer);
			this.XmlWriteNodes(writer);
			writer.WriteEndElement();
		}

		// =========== Protected XML Methods ==================================

		public virtual void XmlReadAttributes(XmlReader reader) 
		{
		}

		public virtual void XmlReadNodes(XmlReader reader) 
		{
			throw new Exception("Unknown NODE -> [" + reader.LocalName + "]");
		}

		public virtual void XmlWriteAttributes(XmlTextWriter writer) 
		{
		}

		public virtual void XmlWriteNodes(XmlTextWriter writer) 
		{
		}
	}
}
