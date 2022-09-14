using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class STQ_Instruction : Instruction 
	{
		public STQ_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "STQ"; } }
	}
}
