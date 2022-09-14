using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class PSHB_Inherent : Instruction, IInherent
	{
		public PSHB_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x37; } }

		public override string Mnemonic { get { return "PSHB"; } }
	}
}
