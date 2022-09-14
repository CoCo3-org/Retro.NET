using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LDY_Immediate : LDY_Instruction, IImmediate 
	{
		public LDY_Immediate(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0x8E; } }
	}
}
