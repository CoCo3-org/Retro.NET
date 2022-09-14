using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class ORB_Instruction : Instruction 
	{
		public ORB_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "ORB"; } }
	}
}
