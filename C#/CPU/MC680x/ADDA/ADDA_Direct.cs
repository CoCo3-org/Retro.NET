using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ADDA_Direct : ADDA_Instruction, IDirect
	{
		public ADDA_Direct(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x9B; } }
	}
}
