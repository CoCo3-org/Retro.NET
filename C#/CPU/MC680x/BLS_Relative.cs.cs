using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class BLS_Relative : Instruction, IRelative
	{
		public BLS_Relative(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x23; } }

		public override string Mnemonic { get { return "BLS"; } }
	}
}
