using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ROR_Indexed : ROR_Instruction, IIndexed
	{
		public ROR_Indexed(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x66; } }
	}
}
