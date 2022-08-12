using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class SBCA_Indexed : SBCA_Instruction, IIndexed
	{
		public override byte OpCode { get { return 0xA2; } }
	}
}
