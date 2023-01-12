//
// Copyright (c) 2023 James Ross
//
// Released under the GNU AFFERO GENERAL PUBLIC LICENSE Version 3
// Please see README.md, LICENSE, agpl-3.0.txt in root folder
//

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
