using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class LDD_Extended : LDD_Instruction, IExtended
	{
		public override byte OpCode { get { return 0xFC; } }
	}
}
