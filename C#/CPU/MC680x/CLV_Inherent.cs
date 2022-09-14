using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class CLV_Inherent : Instruction, IInherent
	{
		public CLV_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x0A; } }

		public override string Mnemonic { get { return "CLV"; } }
	}
}
