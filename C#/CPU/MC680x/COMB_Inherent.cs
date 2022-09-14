using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class COMB_Inherent : Instruction, IInherent
	{
		public COMB_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x53; } }

		public override string Mnemonic { get { return "COMB"; } }
	}
}
