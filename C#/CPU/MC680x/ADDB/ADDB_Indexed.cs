using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ADDB_Indexed : ADDB_Instruction, IIndexed
	{
		public ADDB_Indexed(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0xEB; } }
	}
}
