using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class JMP_Instruction : Instruction
	{
		public JMP_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "JMP"; } }
	}
}
