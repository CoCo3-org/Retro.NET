using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LDF_Instruction : Instruction 
	{
		public LDF_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "LDF"; } }
	}
}
