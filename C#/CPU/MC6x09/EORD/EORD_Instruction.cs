using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class EORD_Instruction : Instruction 
	{
		public EORD_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "EORD"; } }
	}
}
