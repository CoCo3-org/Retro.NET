using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class TSX_Inherent : Instruction, IInherent
	{
		public TSX_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x30; } }

		public override string Mnemonic { get { return "TSX"; } }
	}
}
