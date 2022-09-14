using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ADCB_Instruction : Instruction
	{
		public ADCB_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "ADCB"; } }
	}
}
