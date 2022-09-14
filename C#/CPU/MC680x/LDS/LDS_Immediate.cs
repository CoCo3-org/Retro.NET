using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class LDS_Immediate : LDS_Instruction, IImmediate
	{
		public LDS_Immediate(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x8E; } }
	}
}
