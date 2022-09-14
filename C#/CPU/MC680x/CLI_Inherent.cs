using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class CLI_Inherent : Instruction, IInherent
	{
		public CLI_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x0E; } }

		public override string Mnemonic { get { return "CLI"; } }
	}
}
