using System;
using System.Collections.Generic;
using System.Text;

namespace CPU
{
	public class Instruction : CodeLine
	{
		public Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{

		}

		public virtual byte? PreByte { get { return null; } }
		public virtual byte OpCode { get { throw new NotImplementedException("Type: " + this.GetType()); } } 

		public virtual string Desc { get { return null; } }
		public virtual string Category { get { return null; } }
	}
}
