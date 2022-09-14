using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class ORA_Instruction : Instruction 
	{
		public ORA_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "ORA"; } }
	}
}
