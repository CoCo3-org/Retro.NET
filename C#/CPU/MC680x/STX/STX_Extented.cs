using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class STX_Extended : STX_Instruction, IExtended
	{
		public STX_Extended(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0xFF; } }
	}
}
