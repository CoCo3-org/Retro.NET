using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class ADCR_Register : Instruction, IRegister 
	{
		public ADCR_Register(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0x31; } }

		public override string Mnemonic { get { return "ADCR"; } }
	}
}
