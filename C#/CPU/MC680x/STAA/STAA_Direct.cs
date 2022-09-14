using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class STAA_Direct : STAA_Instruction, IDirect
	{
		public STAA_Direct(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x97; } }
	}
}
