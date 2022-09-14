using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ADDD_Instruction : Instruction
	{
		public ADDD_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "ADDD"; } }
	}
}
