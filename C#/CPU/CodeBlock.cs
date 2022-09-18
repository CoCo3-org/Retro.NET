using System;
using System.Collections.Generic;
using System.Text;

namespace CPU
{
	public class CodeBlock : CodeLine
	{
		public CodeBlock(Module module) 
			: base(module)
		{
		}

		public LinkedList<CodeLine> Lines = new LinkedList<CodeLine>();

		public void AddLine(CodeLine line) { this.Lines.AddLast(line); }

		public override string ToString() 
		{
			StringBuilder sb = new StringBuilder();

			foreach (CodeLine ln in this.Lines)
				sb.AppendLine(ln.ToString());

			return sb.ToString();
		}
	}
}
