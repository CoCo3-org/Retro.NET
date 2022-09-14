using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class NEGA_Inherent : Instruction, IInherent 
	{
		public NEGA_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x40; } }

		public override string Mnemonic { get { return "NEGA"; } }
	}
}
