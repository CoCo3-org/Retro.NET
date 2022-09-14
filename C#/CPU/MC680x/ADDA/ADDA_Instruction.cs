using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ADDA_Instruction : Instruction
	{
		public ADDA_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "ADDA"; } }
	}
}
