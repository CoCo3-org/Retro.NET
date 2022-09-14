using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class BEQ_Relative : Instruction, IRelative
	{
		public BEQ_Relative(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x27; } }

		public override string Mnemonic { get { return "BEQ"; } }
	}
}
