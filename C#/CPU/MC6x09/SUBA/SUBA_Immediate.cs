using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class SUBA_Immediate : SUBA_Instruction, IImmediate 
	{
		public SUBA_Immediate(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x80; } }
	}
}
