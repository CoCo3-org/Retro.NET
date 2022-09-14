using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class OIM_Direct : OIM_Instruction, IDirect 
	{
		public OIM_Direct(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x01; } }
	}
}
