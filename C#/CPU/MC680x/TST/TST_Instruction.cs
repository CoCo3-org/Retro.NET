using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class TST_Instruction : Instruction
	{
		public TST_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "TST"; } }
	}
}
