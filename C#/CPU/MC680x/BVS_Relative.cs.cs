using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class BVS_Relative : Instruction, IRelative
	{
		public BVS_Relative(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x29; } }

		public override string Mnemonic { get { return "BVS"; } }
	}
}
