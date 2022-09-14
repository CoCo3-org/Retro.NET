using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class TFM_TFMRegs2 : TFM_Instruction, ITFMRegs 
	{
		public TFM_TFMRegs2(CodeBlock codeBlock)
			: base(codeBlock)
		{
		}

		public override byte? PreByte { get { return 0x11; } }
		public override byte OpCode { get { return 0x39; } }
	}
}
