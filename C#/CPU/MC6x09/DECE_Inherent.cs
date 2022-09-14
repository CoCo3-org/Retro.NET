using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class DECE_Inherent : Instruction, IInherent 
	{
		public DECE_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x11; } }
		public override byte OpCode { get { return 0x4A; } }

		public override string Mnemonic { get { return "DECE"; } }
	}
}
