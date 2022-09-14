using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ORAB_Instruction : Instruction
	{
		public ORAB_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "ORAB"; } }
	}
}
