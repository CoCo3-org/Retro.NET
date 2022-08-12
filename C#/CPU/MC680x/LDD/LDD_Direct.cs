using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class LDD_Direct : LDD_Instruction, IDirect
	{
		public override byte OpCode { get { return 0xDC; } }
	}
}
