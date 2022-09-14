using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class EORD_Direct : EORD_Instruction, IDirect 
	{
		public EORD_Direct(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0x98; } }
	}
}
