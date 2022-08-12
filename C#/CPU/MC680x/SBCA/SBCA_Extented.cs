using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class SBCA_Extended : SBCA_Instruction, IExtended
	{
		public override byte OpCode { get { return 0xB2; } }
	}
}
