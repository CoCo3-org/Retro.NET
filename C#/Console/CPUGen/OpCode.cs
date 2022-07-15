using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Retro.NET.CPUGen
{
	public class OpCode : CPU.XmlNode
	{
		public int Op { get; set; }
		public string Mnem { get; set; }
		public string Mode { get; set; }
		public int NBytes { get; set; }
		public bool PlusBytes { get; set; }
		public int NCycles { get; set; }
		public bool PlusCycles { get; set; }
		public string Sample { get; set; }

		public override string XmlTag => "OpCode";

		public void Generate(string folder)
		{
			string className = this.Mnem + "_" + this.Mode;
			StreamWriter sw = new StreamWriter(folder + "\\" + className + ".cs");
			sw.WriteLine("// Testing 1,2,3");
			sw.Close();
		}

		public override void XmlWriteAttributes(XmlTextWriter writer)
		{
			base.XmlWriteAttributes(writer);

			writer.WriteAttributeString("Op", this.Op.ToString("X02"));
			writer.WriteAttributeString("Mnem", this.Mnem);
			writer.WriteAttributeString("Mode", this.Mode);
			writer.WriteAttributeString("NBytes", this.NBytes.ToString());
			writer.WriteAttributeString("PlusBytes", this.PlusBytes.ToString());
			writer.WriteAttributeString("NCycles", this.NCycles.ToString());
			writer.WriteAttributeString("PlusCycles", this.PlusCycles.ToString());
			writer.WriteAttributeString("Sample", this.Sample);
		}
	}
}
