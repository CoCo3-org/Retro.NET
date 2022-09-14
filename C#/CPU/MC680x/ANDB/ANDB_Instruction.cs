using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ANDB_Instruction : Instruction
	{
		public ANDB_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "ANDB"; } }
	}
}
