using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class STW_Instruction : Instruction 
	{
		public STW_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "STW"; } }
	}
}
