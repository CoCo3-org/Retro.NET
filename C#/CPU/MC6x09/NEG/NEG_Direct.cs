using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class NEG_Direct : NEG_Instruction, IDirect 
	{
		public NEG_Direct(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x00; } }
	}
}
