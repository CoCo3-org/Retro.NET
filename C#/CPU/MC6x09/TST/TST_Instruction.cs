using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class TST_Instruction : Instruction 
	{
		public TST_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "TST"; } }
	}
}
