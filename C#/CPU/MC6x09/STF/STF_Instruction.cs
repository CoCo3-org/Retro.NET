using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class STF_Instruction : Instruction 
	{
		public STF_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "STF"; } }
	}
}
