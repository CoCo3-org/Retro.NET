using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class BITA_Instruction : Instruction
	{
		public BITA_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "BITA"; } }
	}
}
