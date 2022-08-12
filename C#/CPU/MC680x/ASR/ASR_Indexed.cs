using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ASR_Indexed : ASR_Instruction, IIndexed
	{
		public override byte OpCode { get { return 0x67; } }
	}
}
