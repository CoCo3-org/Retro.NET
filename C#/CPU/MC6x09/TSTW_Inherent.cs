using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class TSTW_Inherent : Instruction, IInherent 
	{
		public TSTW_Inherent(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}
		
		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0x5D; } }

		public override string Mnemonic { get { return "TSTW"; } }
	}
}
