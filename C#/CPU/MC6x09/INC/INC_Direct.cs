using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class INC_Direct : INC_Instruction, IDirect 
	{
		public INC_Direct(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x0C; } }
	}
}
