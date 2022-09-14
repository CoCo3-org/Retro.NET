using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class TST_Extended : TST_Instruction, IExtended 
	{
		public TST_Extended(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x7D; } }
	}
}
