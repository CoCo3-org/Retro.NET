using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LSLA_Inherent : Instruction, IInherent 
	{
		public LSLA_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x48; } }

		public override string Mnemonic { get { return "LSLA"; } }
	}
}
