using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class ADCA_Extended : ADCA_Instruction, IExtended
	{
		public override byte OpCode { get { return 0xB9; } }
	}
}
