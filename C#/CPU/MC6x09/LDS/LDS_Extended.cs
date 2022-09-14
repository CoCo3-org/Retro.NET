using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LDS_Extended : LDS_Instruction, IExtended 
	{
		public LDS_Extended(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0xFE; } }
	}
}
