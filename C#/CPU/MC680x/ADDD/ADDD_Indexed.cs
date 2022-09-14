using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ADDD_Indexed : ADDD_Instruction, IIndexed
	{
		public ADDD_Indexed(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0xE3; } }
	}
}
