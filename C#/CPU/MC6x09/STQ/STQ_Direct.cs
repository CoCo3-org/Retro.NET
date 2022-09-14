using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class STQ_Direct : STQ_Instruction, IDirect 
	{
		public STQ_Direct(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0xDD; } }
	}
}
