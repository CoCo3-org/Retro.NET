using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class TIM_Instruction : Instruction 
	{
		public TIM_Instruction(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override string Mnemonic { get { return "TIM"; } }
	}
}
