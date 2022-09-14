using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class STD_Direct : STD_Instruction, IDirect
	{
		public STD_Direct(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0xDD; } }
	}
}
