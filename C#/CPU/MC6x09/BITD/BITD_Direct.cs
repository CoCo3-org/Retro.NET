using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class BITD_Direct : BITD_Instruction, IDirect 
	{
		public BITD_Direct(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0x95; } }
	}
}
