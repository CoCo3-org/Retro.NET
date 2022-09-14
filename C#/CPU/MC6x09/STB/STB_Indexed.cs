using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class STB_Indexed : STB_Instruction, IIndexed 
	{
		public STB_Indexed(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0xE7; } }
	}
}
