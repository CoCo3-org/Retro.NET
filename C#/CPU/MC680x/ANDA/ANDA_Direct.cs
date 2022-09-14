using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ANDA_Direct : ANDA_Instruction, IDirect
	{
		public ANDA_Direct(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x94; } }
	}
}
