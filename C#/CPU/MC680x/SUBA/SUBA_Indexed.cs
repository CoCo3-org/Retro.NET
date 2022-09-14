using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class SUBA_Indexed : SUBA_Instruction, IIndexed
	{
		public SUBA_Indexed(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0xA0; } }
	}
}
