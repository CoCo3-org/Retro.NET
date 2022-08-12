using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LDQ_Extended : LDQ_Instruction, IExtended 
	{
		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0xFC; } }
	}
}
