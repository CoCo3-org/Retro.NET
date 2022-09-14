using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class INCB_Inherent : Instruction, IInherent 
	{
		public INCB_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x5C; } }

		public override string Mnemonic { get { return "INCB"; } }
	}
}
