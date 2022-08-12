using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC680x
{
	public class SUBD_Extended : SUBD_Instruction, IExtended
	{
		public override byte OpCode { get { return 0xB3; } }
	}
}
