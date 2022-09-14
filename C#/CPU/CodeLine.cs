using System;
using System.Collections.Generic;
using System.Text;

namespace CPU
{
	public class CodeLine : XmlNode
	{
		public CodeLine(CodeBlock codeBlock) 
			: base(codeBlock)
		{

		}

		public Label Label { get; set; }
		public int Address { get; set; }
		public virtual string Mnemonic { get { return null; } }


	}
}
