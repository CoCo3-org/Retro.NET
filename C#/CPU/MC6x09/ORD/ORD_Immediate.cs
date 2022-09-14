using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class ORD_Immediate : ORD_Instruction, IImmediate 
	{
		public ORD_Immediate(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0x8A; } }
	}
}
