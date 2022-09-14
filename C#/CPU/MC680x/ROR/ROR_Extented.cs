using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ROR_Extended : ROR_Instruction, IExtended
	{
		public ROR_Extended(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x76; } }
	}
}
