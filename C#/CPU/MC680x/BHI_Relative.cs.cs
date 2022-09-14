using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class BHI_Relative : Instruction, IRelative
	{
		public BHI_Relative(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x22; } }

		public override string Mnemonic { get { return "BHI"; } }
	}
}
