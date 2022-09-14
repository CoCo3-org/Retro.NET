using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class STX_Indexed : STX_Instruction, IIndexed
	{
		public STX_Indexed(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0xEF; } }
	}
}
