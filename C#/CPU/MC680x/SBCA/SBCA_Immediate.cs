using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class SBCA_Immediate : SBCA_Instruction, IImmediate
	{
		public SBCA_Immediate(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x82; } }
	}
}
