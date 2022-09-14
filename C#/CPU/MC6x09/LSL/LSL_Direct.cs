using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LSL_Direct : LSL_Instruction, IDirect 
	{
		public LSL_Direct(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x08; } }
	}
}
