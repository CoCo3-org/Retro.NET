using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class STS_Direct : STS_Instruction, IDirect
	{
		public STS_Direct(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x9F; } }
	}
}
