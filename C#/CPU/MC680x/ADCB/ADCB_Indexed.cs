using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ADCB_Indexed : ADCB_Instruction, IIndexed
	{
		public ADCB_Indexed(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0xE9; } }
	}
}
