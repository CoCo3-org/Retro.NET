using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class LDX_Immediate : LDX_Instruction, IImmediate 
	{
		public override byte OpCode { get { return 0x8E; } }
	}
}
