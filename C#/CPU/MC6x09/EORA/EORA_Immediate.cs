using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class EORA_Immediate : EORA_Instruction, IImmediate 
	{
		public EORA_Immediate(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x88; } }
	}
}
