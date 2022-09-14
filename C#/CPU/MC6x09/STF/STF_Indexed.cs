using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class STF_Indexed : STF_Instruction, IIndexed 
	{
		public STF_Indexed(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x11; } }
		public override byte OpCode { get { return 0xE7; } }
	}
}
