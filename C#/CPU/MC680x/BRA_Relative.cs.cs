using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class BRA_Relative : Instruction, IRelative
	{
		public BRA_Relative(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x20; } }

		public override string Mnemonic { get { return "BRA"; } }
	}
}
