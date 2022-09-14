using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class TFR_Immediate : Instruction, IImmediate 
	{
		public TFR_Immediate(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte OpCode { get { return 0x1F; } }

		public override string Mnemonic { get { return "TFR"; } }
	}
}
