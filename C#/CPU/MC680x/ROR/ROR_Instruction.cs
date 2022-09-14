using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ROR_Instruction : Instruction
	{
		public ROR_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "ROR"; } }
	}
}
