using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ADCA_Immediate : ADCA_Instruction, IImmediate
	{
		public ADCA_Immediate(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x89; } }
	}
}
