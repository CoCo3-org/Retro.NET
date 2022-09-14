using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LDE_Instruction : Instruction 
	{
		public LDE_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "LDE"; } }
	}
}
