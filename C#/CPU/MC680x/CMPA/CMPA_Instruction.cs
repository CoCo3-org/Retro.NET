using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class CMPA_Instruction : Instruction
	{
		public CMPA_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "CMPA"; } }
	}
}
