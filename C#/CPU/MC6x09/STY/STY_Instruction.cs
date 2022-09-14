using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class STY_Instruction : Instruction 
	{
		public STY_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "STY"; } }
	}
}
