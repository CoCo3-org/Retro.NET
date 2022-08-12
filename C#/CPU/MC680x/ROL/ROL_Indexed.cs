using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ROL_Indexed : ROL_Instruction, IIndexed
	{
		public override byte OpCode { get { return 0x69; } }
	}
}
