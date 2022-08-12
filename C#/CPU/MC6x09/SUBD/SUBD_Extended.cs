using System;
using System.Collections.Generic;
using System.Text;

namespace CPU.MC6x09
{
	public class SUBD_Extended : SUBD_Instruction, IExtended 
	{
		public override byte OpCode { get { return 0xB3; } }
	}
}
