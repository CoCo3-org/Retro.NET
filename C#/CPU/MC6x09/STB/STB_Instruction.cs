using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class STB_Instruction : Instruction 
	{
		public STB_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "STB"; } }
	}
}
