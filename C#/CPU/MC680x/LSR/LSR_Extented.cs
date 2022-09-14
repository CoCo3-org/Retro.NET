using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class LSR_Extended : LSR_Instruction, IExtended
	{
		public LSR_Extended(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x74; } }
	}
}
