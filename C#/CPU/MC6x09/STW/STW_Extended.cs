using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class STW_Extended : STW_Instruction, IExtended 
	{
		public STW_Extended(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0xB7; } }
	}
}
