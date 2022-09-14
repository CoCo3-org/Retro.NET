using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class BCC_Relative : Instruction, IRelative
	{
		public BCC_Relative(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x24; } }

		public override string Mnemonic { get { return "BCC"; } }
	}
}
