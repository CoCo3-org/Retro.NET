using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class INC_Indexed : INC_Instruction, IIndexed
	{
		public INC_Indexed(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x6C; } }
	}
}
