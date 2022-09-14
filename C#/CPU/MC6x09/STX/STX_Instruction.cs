using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class STX_Instruction : Instruction 
	{
		public STX_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "STX"; } }
	}
}
