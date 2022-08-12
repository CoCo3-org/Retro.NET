using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LDW_Immediate : LDW_Instruction, IImmediate 
	{
		public override byte? PreByte { get { return 0x10; } }
		public override byte OpCode { get { return 0x86; } }
	}
}
