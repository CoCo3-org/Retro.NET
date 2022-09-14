using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class CLR_Indexed : CLR_Instruction, IIndexed
	{
		public CLR_Indexed(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x6F; } }
	}
}
