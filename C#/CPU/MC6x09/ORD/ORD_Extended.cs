using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class ORD_Extended : ORD_Instruction, IExtended 
	{
		public ORD_Extended(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0xBA; } }
	}
}
