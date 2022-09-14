using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class EORB_Direct : EORB_Instruction, IDirect 
	{
		public EORB_Direct(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0xD8; } }
	}
}
