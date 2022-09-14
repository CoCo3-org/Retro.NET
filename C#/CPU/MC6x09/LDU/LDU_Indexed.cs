using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LDU_Indexed : LDU_Instruction, IIndexed 
	{
		public LDU_Indexed(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0xEE; } }
	}
}
