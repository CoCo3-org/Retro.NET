using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LDB_Instruction : Instruction 
	{
		public LDB_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "LDB"; } }
	}
}
