using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class LDAB_Instruction : Instruction
	{
		public LDAB_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "LDAB"; } }
	}
}
