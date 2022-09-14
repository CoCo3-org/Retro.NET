using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LDE_Extended : LDE_Instruction, IExtended 
	{
		public LDE_Extended(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x11; } }
		public override byte OpCode { get { return 0xB6; } }
	}
}
