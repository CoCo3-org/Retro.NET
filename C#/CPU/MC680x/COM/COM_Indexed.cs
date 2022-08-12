using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class COM_Indexed : COM_Instruction, IIndexed
	{
		public override byte OpCode { get { return 0x63; } }
	}
}
