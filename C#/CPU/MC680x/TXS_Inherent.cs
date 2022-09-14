using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class TXS_Inherent : Instruction, IInherent
	{
		public TXS_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x35; } }

		public override string Mnemonic { get { return "TXS"; } }
	}
}
