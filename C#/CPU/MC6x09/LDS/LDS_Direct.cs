using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LDS_Direct : LDS_Instruction, IDirect 
	{
		public LDS_Direct(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0xDE; } }
	}
}
