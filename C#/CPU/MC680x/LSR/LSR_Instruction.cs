using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class LSR_Instruction : Instruction
	{
		public LSR_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "LSR"; } }
	}
}
