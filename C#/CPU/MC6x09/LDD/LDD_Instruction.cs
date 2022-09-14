using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LDD_Instruction : Instruction 
	{
		public LDD_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "LDD"; } }
	}
}
