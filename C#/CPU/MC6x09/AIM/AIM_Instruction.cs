using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class AIM_Instruction : Instruction 
	{
		public AIM_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "AIM"; } }
	}
}
