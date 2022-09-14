using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class DEC_Indexed : DEC_Instruction, IIndexed
	{
		public DEC_Indexed(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x6A; } }
	}
}
