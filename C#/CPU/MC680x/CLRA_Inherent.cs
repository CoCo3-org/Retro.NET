using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class CLRA_Inherent : Instruction, IInherent
	{
		public CLRA_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x4F; } }

		public override string Mnemonic { get { return "CLRA"; } }
	}
}
