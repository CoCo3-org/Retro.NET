using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LDW_Instruction : Instruction 
	{
		public LDW_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "LDW"; } }
	}
}
