using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class INC_Instruction : Instruction
	{
		public INC_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "INC"; } }
	}
}
