using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LDE_Direct : LDE_Instruction, IDirect 
	{
		public LDE_Direct(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x11; } }
		public override byte OpCode { get { return 0x96; } }
	}
}
