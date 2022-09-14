using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class STS_Extended : STS_Instruction, IExtended
	{
		public STS_Extended(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0xBF; } }
	}
}
