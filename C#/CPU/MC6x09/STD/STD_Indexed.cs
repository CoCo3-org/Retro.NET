using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class STD_Indexed : STD_Instruction, IIndexed 
	{
		public STD_Indexed(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0xED; } }
	}
}
