using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class STX_Instruction : Instruction
	{
		public STX_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "STX"; } }
	}
}
