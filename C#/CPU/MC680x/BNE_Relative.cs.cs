using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class BNE_Relative : Instruction, IRelative
	{
		public BNE_Relative(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x26; } }

		public override string Mnemonic { get { return "BNE"; } }
	}
}
