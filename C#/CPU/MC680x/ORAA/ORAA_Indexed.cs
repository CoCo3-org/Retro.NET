using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ORAA_Indexed : ORAA_Instruction, IIndexed
	{
		public ORAA_Indexed(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0xAA; } }
	}
}
