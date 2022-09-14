using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class MULD_Instruction : Instruction 
	{
		public MULD_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "MULD"; } }
	}
}
