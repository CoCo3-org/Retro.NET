using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class DECB_Inherent : Instruction, IInherent 
	{
		public DECB_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x5A; } }

		public override string Mnemonic { get { return "DECB"; } }
	}
}
