using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ANDA_Extended : ANDA_Instruction, IExtended
	{
		public ANDA_Extended(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0xB4; } }
	}
}
