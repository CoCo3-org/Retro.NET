using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LDU_Instruction : Instruction 
	{
		public LDU_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "LDU"; } }
	}
}
