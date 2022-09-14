using System;
using System.Collections.Generic;
using System.Text;

namespace CPU
{
	public class Label : XmlNode
	{
		public string Text { get; set; }
		public CodeLine CodeLine { get; set; }

		public Label(CodeLine codeLine)  
			: base(codeLine)
		{
			this.CodeLine = codeLine;
		}
	}
}
