using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class ASR_Instruction : Instruction 
	{
		public ASR_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "ASR"; } }
	}
}
