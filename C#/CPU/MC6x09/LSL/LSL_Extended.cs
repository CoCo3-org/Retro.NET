using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LSL_Extended : LSL_Instruction, IExtended 
	{
		public LSL_Extended(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x78; } }
	}
}
