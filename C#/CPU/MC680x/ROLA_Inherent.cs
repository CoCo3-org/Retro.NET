using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ROLA_Inherent : Instruction, IInherent
	{
		public ROLA_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x49; } }

		public override string Mnemonic { get { return "ROLA"; } }
	}
}
