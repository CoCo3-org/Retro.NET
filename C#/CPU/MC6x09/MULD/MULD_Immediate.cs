using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class MULD_Immediate : MULD_Instruction, IImmediate 
	{
		public MULD_Immediate(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x11; } }
		public override byte OpCode { get { return 0x8F; } }
	}
}
