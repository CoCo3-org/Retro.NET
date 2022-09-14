using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class CPX_Immediate : CPX_Instruction, IImmediate
	{
		public CPX_Immediate(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x8C; } }
	}
}
