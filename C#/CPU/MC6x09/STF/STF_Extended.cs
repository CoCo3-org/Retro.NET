using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class STF_Extended : STF_Instruction, IExtended 
	{
		public STF_Extended(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x11; } }
		public override byte OpCode { get { return 0xF7; } }
	}
}
