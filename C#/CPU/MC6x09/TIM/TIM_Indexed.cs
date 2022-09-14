using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class TIM_Indexed : TIM_Instruction, IIndexed 
	{
		public TIM_Indexed(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x6B; } }
	}
}
