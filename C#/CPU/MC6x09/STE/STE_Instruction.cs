using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class STE_Instruction : Instruction 
	{
		public STE_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "STE"; } }
	}
}
