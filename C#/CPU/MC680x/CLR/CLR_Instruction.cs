using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class CLR_Instruction : Instruction
	{
		public CLR_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "CLR"; } }
	}
}
