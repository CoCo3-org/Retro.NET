using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class STE_Direct : STE_Instruction, IDirect 
	{
		public STE_Direct(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x11; } }
		public override byte OpCode { get { return 0x97; } }
	}
}
