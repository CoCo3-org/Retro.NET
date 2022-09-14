using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ANDB_Indexed : ANDB_Instruction, IIndexed
	{
		public ANDB_Indexed(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0xE4; } }
	}
}
