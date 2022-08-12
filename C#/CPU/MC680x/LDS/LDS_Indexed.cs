using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class LDS_Indexed : LDS_Instruction, IIndexed
	{
		public override byte OpCode { get { return 0xAE; } }
	}
}
