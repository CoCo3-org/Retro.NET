using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class EORB_Instruction : Instruction 
	{
		public EORB_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "EORB"; } }
	}
}
