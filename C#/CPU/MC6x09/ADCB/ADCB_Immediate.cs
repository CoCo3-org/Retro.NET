using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class ADCB_Immediate : ADCB_Instruction, IImmediate 
	{
		public ADCB_Immediate(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0xC9; } }
	}
}
