using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LSL_Indexed : LSL_Instruction, IIndexed 
	{
		public LSL_Indexed(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x68; } }
	}
}
