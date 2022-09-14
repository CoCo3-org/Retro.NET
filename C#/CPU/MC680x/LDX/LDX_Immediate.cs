using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class LDX_Immediate : LDX_Instruction, IImmediate
	{
		public LDX_Immediate(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0xCE; } }
	}
}
