using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class LDS_Extended : LDS_Instruction, IExtended
	{
		public override byte OpCode { get { return 0xBE; } }
	}
}
