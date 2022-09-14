using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class STS_Instruction : Instruction 
	{
		public STS_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "STS"; } }
	}
}
