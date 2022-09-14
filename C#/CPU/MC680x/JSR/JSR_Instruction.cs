using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
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
