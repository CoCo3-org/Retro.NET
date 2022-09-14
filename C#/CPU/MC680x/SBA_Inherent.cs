using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class SBA_Inherent : Instruction, IInherent
	{
		public SBA_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x10; } }

		public override string Mnemonic { get { return "SBA"; } }
	}
}
