using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LDB_Indexed : LDB_Instruction, IIndexed 
	{
		public LDB_Indexed(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0xE6; } }
	}
}
