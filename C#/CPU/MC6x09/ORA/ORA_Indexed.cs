using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class ORA_Indexed : ORA_Instruction, IIndexed 
	{
		public ORA_Indexed(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0xAA; } }
	}
}
