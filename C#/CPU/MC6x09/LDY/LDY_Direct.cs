using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LDY_Direct : LDY_Instruction, IDirect 
	{
		public LDY_Direct(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0x9E; } }
	}
}
