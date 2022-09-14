using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class SBCB_Instruction : Instruction
	{
		public SBCB_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "SBCB"; } }
	}
}
