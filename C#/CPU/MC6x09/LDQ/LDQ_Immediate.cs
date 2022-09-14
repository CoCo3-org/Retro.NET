using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LDQ_Immediate : LDQ_Instruction, IImmediate 
	{
		public LDQ_Immediate(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0xCD; } }
	}
}
