using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class STU_Direct : STU_Instruction, IDirect 
	{
		public STU_Direct(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0xDF; } }
	}
}
