using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LDQ_Instruction : Instruction 
	{
		public LDQ_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "LDQ"; } }
	}
}
