using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class SBCA_Direct : SBCA_Instruction, IDirect
	{
		public SBCA_Direct(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x92; } }
	}
}
