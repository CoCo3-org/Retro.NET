using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class ORD_Instruction : Instruction 
	{
		public ORD_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "ORD"; } }
	}
}
