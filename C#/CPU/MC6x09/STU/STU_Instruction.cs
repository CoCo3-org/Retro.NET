using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class STU_Instruction : Instruction 
	{
		public STU_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "STU"; } }
	}
}
