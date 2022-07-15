using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Xml;

namespace Retro.NET.CPUGen
{
	public class CPUClass : CPU.XmlNode
	{
		public List<OpCode> OpCodes = new List<OpCode>();

		public virtual void Generate(string folder) 
		{
			foreach(OpCode op in OpCodes)
			{
				op.Generate(folder);
			}
		}

		public void ReadXmlFile(string path) 
		{
			FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
			XmlTextReader reader = new XmlTextReader(fs);

			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.Whitespace) continue;

				// check for EOF
				if (reader.NodeType == XmlNodeType.EndElement && reader.LocalName == this.XmlTag)
					break;

				if (reader.NodeType == XmlNodeType.Element && reader.LocalName == this.XmlTag)
				{
					// _name = reader.GetAttribute("ID"); // JAR_NOTE: name is passed on creation .... 
					this.ReadXml(reader);
				}
			}

			reader.Close();
			fs.Close();
		}

		public void WriteXmlFile(string xmlPath) 
		{
			FileStream fs = new FileStream(xmlPath, FileMode.Create, FileAccess.Write);
			XmlTextWriter writer = new XmlTextWriter(fs, Encoding.UTF8);
			writer.Formatting = Formatting.Indented;
			this.WriteXml(writer);
			writer.Close();
			fs.Close();
		}

		public override void XmlWriteNodes(XmlTextWriter writer)
		{
			base.XmlWriteNodes(writer);
			foreach (OpCode op in this.OpCodes)
				op.WriteXml(writer);
		}
	}
}
