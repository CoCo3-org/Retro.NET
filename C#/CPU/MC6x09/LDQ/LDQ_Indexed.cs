using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LDQ_Indexed : LDQ_Instruction, IIndexed 
	{
		public LDQ_Indexed(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0xEC; } }
	}
}
