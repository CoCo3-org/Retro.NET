using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ADCA_Direct : ADCA_Instruction, IDirect
	{
		public ADCA_Direct(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x99; } }
	}
}
