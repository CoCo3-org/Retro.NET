using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class JSR_Instruction : Instruction 
	{
		public JSR_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "JSR"; } }
	}
}
