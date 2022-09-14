using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class WAI_Inherent : Instruction, IInherent
	{
		public WAI_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x3E; } }

		public override string Mnemonic { get { return "WAI"; } }
	}
}
