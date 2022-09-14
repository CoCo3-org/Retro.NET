using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class TSTA_Inherent : Instruction, IInherent
	{
		public TSTA_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x4D; } }

		public override string Mnemonic { get { return "TSTA"; } }
	}
}
