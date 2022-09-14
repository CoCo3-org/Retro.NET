using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class BCS_Relative : Instruction, IRelative
	{
		public BCS_Relative(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x25; } }

		public override string Mnemonic { get { return "BCS"; } }
	}
}
