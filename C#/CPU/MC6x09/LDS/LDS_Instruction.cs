using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LDS_Instruction : Instruction 
	{
		public LDS_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "LDS"; } }
	}
}
