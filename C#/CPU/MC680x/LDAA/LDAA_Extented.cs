using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class LDAA_Extended : LDAA_Instruction, IExtended
	{
		public LDAA_Extended(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0xB6; } }
	}
}
