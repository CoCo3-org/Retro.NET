using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class STAB_Instruction : Instruction
	{
		public STAB_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "STAB"; } }
	}
}
