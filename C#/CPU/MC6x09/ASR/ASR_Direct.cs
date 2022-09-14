using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class ASR_Direct : ASR_Instruction, IDirect 
	{
		public ASR_Direct(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x07; } }
	}
}
