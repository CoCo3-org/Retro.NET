using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class RTS_Inherent : Instruction, IInherent 
	{
		public RTS_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x39; } }

		public override string Mnemonic { get { return "RTS"; } }
	}
}
