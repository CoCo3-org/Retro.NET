using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class DEC_Instruction : Instruction
	{
		public DEC_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "DEC"; } }
	}
}
